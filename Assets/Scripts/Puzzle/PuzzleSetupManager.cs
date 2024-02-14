using System.Linq;
using Autohand;
using Puzzle.Scriptables;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Puzzle
{
    public class PuzzleSetupManager : MonoBehaviour
    {
        [SerializeField] private ComputeShader comp;
        [SerializeField] private PuzzleBoardDimensions boardDimensions;
        [SerializeField] private BoolVariable state;

        [SerializeField] private PuzzleImageQuads puzzleImageQuads;

        [SerializeField, Space] private GameObject puzzlePiece;
        [SerializeField] private GameObject board;
        [SerializeField] private GameObject placePoint;

        [SerializeField, Space] private SelectedImage selectedImage;
        [SerializeField] private GoalSprite goalSprite;
        [SerializeField] private Placed placed;

        public UnityEvent changedImage;
        public UnityEvent completed;

        private Vector3 scale;
        private Vector3 ppSize; //place point size
        private GameObject[] points;
        private GameObject[] pieces;
        private int av;
        private Vector3 bSize; //board size
        private Texture2D tex;
        private int prevPlaced;
        //private int[,] indexes;
        private int[] indPos;

        private bool[] correct;

        /*private GraphicsBuffer squares;
        private GraphicsBuffer rearrangedSquares;
        private GraphicsBuffer buffer;*/

        private int _compareImageKernelIndex;

        private void Awake()
        {
            ppSize = placePoint.GetComponent<Renderer>().bounds.size;
            bSize = board.GetComponent<Renderer>().bounds.size;
            bSize.y = bSize.x;
            ppSize.y = ppSize.x;
            ppSize *= 14.1f;
            scale = board.transform.localScale;
            placed.amount = 0;

            _compareImageKernelIndex = comp.FindKernel("CompareImages");
        }

        /*private void SetupBuffers()
        {
            squares = new GraphicsBuffer(GraphicsBuffer.Target.Structured, tex.height * tex.width, sizeof(float));
            rearrangedSquares =
                new GraphicsBuffer(GraphicsBuffer.Target.Structured, tex.height * tex.width, sizeof(float));
            buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, tex.width * tex.height, sizeof(float));
        }*/

        /*private void OnDisable()
        {
            buffer?.Dispose();
            squares?.Dispose();
            rearrangedSquares?.Dispose();
        }*/

        public void SetUp()
        {
            //OnDisable();
            SetTexture();
            //IndexArray();
            ScaleBoard();
            PlacePoints();
            //SetupBuffers();
        }

        private void SetTexture()
        {
            tex = selectedImage.currentSelected;

            comp.SetTexture(_compareImageKernelIndex, PuzzleShaderVariables.Image, tex);
            comp.SetInt(PuzzleShaderVariables.Height, tex.height);
            comp.SetInt(PuzzleShaderVariables.Width, tex.width);
            comp.SetInt(PuzzleShaderVariables.PixelCount, tex.width * tex.height);
        }

        /*private void IndexArray()
        {
            indexes = new int[tex.height, tex.width];
            var a = 0;

            for (var i = 0; i < tex.height; i++)
            for (var j = 0; j < tex.width; j++)
            {
                indexes[i, j] = a++;
            }
        }*/

        private void ScaleBoard()
        {
            board.transform.localScale = new Vector3(
                boardDimensions.Width * scale.x,
                boardDimensions.Height * scale.y,
                scale.z);
        }

        private void PlacePoints()
        {
            if (points != null)
            {
                foreach (var point in points)
                {
                    Destroy(point);
                }
            }

            var boardHeight = boardDimensions.Height;
            var boardWidth = boardDimensions.Width;

            points = new GameObject[boardHeight * boardWidth];

            var trpp = placePoint.transform; //transform place point
            var grid = new Vector3[boardHeight, boardWidth];

            var originVectorX = -ppSize.x * (boardWidth / 2) + (1 - boardWidth % 2) * ppSize.x / 2f;
            var originVectorY = -ppSize.y * (boardHeight / 2) + (1 - boardHeight % 2) * ppSize.y / 2f;

            for (var i = 0; i < boardHeight; i++)
            {
                for (var j = 0; j < boardWidth; j++)
                {
                    grid[i, j] = new Vector3(
                        originVectorX + j * ppSize.x,
                        originVectorY + i * ppSize.y,
                        trpp.localPosition.z);
                }
            }

            var available = 0;
            indPos = new int[boardHeight*boardWidth];
            correct = new bool[boardHeight*boardWidth];
            
            for (var i = 0; i < boardHeight; i++)
            {
                for (var j = 0; j < boardWidth; j++)
                {
                    var obj = Instantiate(placePoint);
                    points[available] = obj;
                    obj.SetActive(true);
                    obj.name = available + "";

                    var tr = obj.transform;
                    tr.SetParent(gameObject.transform);
                    tr = obj.transform;

                    var s = Quaternion.AngleAxis(-60, Vector3.forward) * tr.up;

                    var p = s * grid[i, j].y;
                    tr.localPosition = p + tr.right * grid[i, j].x;
                    tr.rotation = trpp.rotation;
                    indPos[available] = boardWidth + j - i*boardWidth;
                    available++;
                }
            }

            /*var a = 0;
            indPos = new int[boardWidth * boardHeight];

            for (var i = available - boardWidth; i >= 0; i -= boardWidth)
            {
                for (var j = 0; j < boardWidth; j++)
                {
                    indPos[a] = i + j;
                    a++;
                }
            }*/
        }

        /*private void ShuffleIndex()
        {
            var pos = int.Parse(placed.number);
            var point = points[pos];
            var child = point.GetComponent<PlacePoint>().GetPlacedObject();
            var pieceQuad = int.Parse(child.name);
            var quad = indPos[pos];
            puzzleImageQuads.rearrangedQuads[quad] = puzzleImageQuads.quads[pieceQuad];
        }*/

        private void CheckPiecePos()
        {
            var pos = int.Parse(placed.number);
            var point = points[pos];
            var pointN = indPos[pos];
            var child = point.GetComponent<PlacePoint>().GetPlacedObject();
            var pieceN = int.Parse(child.name);
            correct[pieceN] = pointN == pieceN;
        }

        public void CompareImage()
        {
            //ShuffleIndex();
            CheckPiecePos();

            var height = boardDimensions.Height;
            var width = boardDimensions.Width;

            if (placed.amount != height * width)
            {
                return;
            }

            /*var texDim = tex.height * tex.width;
            var quadsFlat = new int[texDim];
            var rQuadsFlat = new int[texDim];
            var pos = 0;

            for (var quadIndex = 0; quadIndex < puzzleImageQuads.quads.Length; quadIndex++)
            for (var h = 0; h < tex.height / height; h++)
            for (var w = 0; w < tex.width / width; w++)
            {
                quadsFlat[pos] = puzzleImageQuads.quads[quadIndex].rows[h, w];
                rQuadsFlat[pos] = puzzleImageQuads.rearrangedQuads[quadIndex].rows[h, w];
                pos++;
            }

            squares.SetData(quadsFlat);
            rearrangedSquares.SetData(rQuadsFlat);
            
            comp.SetBuffer(_compareImageKernelIndex, PuzzleShaderVariables.Result, buffer);
            comp.SetBuffer(_compareImageKernelIndex, PuzzleShaderVariables.Quads, squares);
            comp.SetBuffer(_compareImageKernelIndex, PuzzleShaderVariables.RearrangedQuads, rearrangedSquares);

            comp.GetKernelThreadGroupSizes(_compareImageKernelIndex, out var x, out var y , out var z);
            comp.Dispatch(_compareImageKernelIndex, Mathf.CeilToInt((float)texDim / x), (int)y, (int)z);
            
            var result = new int[texDim];
            buffer.GetData(result);*/
            
            

            if (correct.Any(val => val != true))
            {
                state.value = false;
                completed.Invoke();
                return;
            }

            state.value = true;
            completed.Invoke();
        }
    }
}