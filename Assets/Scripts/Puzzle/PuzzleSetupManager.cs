using System;
using System.Linq;
using Autohand;
using Puzzle.Scriptables;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Puzzle
{
    public class PuzzleSetupManager : MonoBehaviour
    {
        [SerializeField] private ComputeShader comp;
        [SerializeField] private IntVariable height;
        [SerializeField] private IntVariable width;
        [SerializeField] private BoolVariable state;

        [SerializeField] private GameObject puzzlePiece;
        [SerializeField] private GameObject board;
        [SerializeField] private GameObject placePoint;
        [SerializeField] private SelectedImage selectedImage;
        [SerializeField] private GoalSprite goalSprite;

        [SerializeField] private Placed placed;

        [SerializeField] private UnityEvent changedImage;
        [SerializeField] private UnityEvent completed;


        private Vector3 scale;
        private Vector3 ppSize; //place point size
        private GameObject[] points;
        private GameObject[] pieces;
        private int available;
        private int av;
        private Vector3 bSize; //board size
        private Texture2D tex;
        private int prevPlaced;
        private int[,] indexes;
        private int[] indPos;
        private Quad[] rearrangedQuads;
        private Quad[] quads;

        private GraphicsBuffer squares;
        private GraphicsBuffer rearrangedSquares;
        private GraphicsBuffer buffer;

        private struct Quad
        {
            public int[,] rows;
        }

        private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");
        private static readonly int Image = Shader.PropertyToID("image");
        private static readonly int Ri = Shader.PropertyToID("ri"); //render image in compute shader
        private static readonly int Result = Shader.PropertyToID("Result");
        private static readonly int Quads = Shader.PropertyToID("quads");
        private static readonly int RearrangedQuads = Shader.PropertyToID("rearrangedQuads");
        private static readonly int Width = Shader.PropertyToID("width");
        private static readonly int Height = Shader.PropertyToID("height");

        private void Awake()
        {
            ppSize = placePoint.GetComponent<Renderer>().bounds.size;
            bSize = board.GetComponent<Renderer>().bounds.size;
            bSize.y = bSize.x;
            ppSize.y = ppSize.x;
            ppSize *= 14.1f;
            scale = board.transform.localScale;
            SetUp();
        }

        private void OnEnable()
        {
            squares = new GraphicsBuffer(GraphicsBuffer.Target.Structured, tex.height * tex.width, sizeof(float));
            rearrangedSquares =
                new GraphicsBuffer(GraphicsBuffer.Target.Structured, tex.height * tex.width, sizeof(float));
            buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, tex.width * tex.height, sizeof(float));
        }

        private void OnDisable()
        {
            buffer.Release();
            squares.Release();
            rearrangedSquares.Release();
        }

        public void SetUp()
        {
            IndexArray();
            ScaleBoard();
            PlacePoints();
            SetUpPieces();
            GoalSprite();
            OnEnable();
        }

        private void IndexArray()
        {
            tex = selectedImage.currentSelected;

            indexes = new int[tex.height, tex.width];
            var a = 0;
            for (var i = 0; i < tex.height; i++)
            {
                for (var j = 0; j < tex.width; j++)
                {
                    indexes[i, j] = a;
                    a++;
                }
            }
        }

        private void PlacePoints()
        {
            var trpp = placePoint.transform; //transform place point
            var pos = trpp.localPosition;
            var grid = new Vector3[height.value, width.value];
            if (points != null)
            {
                foreach (var point in points)
                {
                    Destroy(point);
                }
            }

            points = new GameObject[height.value * width.value];

            var originVector = new Vector3();
            available = 0;
            
            if (height.value % 2 == 0)
            {
                originVector.y = -ppSize.y * (height.value / 2) + ppSize.y / 2;
            }
            else
            {
                originVector.y = -ppSize.y * (height.value / 2);
            }

            if (width.value % 2 == 0)
            {
                originVector.x =  -ppSize.x * (width.value / 2) + ppSize.x / 2;
            }
            else
            {
                originVector.x = -ppSize.x * (width.value / 2);
            }

            for (var i = 0; i < height.value; i++)
            {
                for (var j = 0; j < width.value; j++)
                {
                    grid[i, j] = new Vector3(originVector.x + j * ppSize.x, originVector.y + i * ppSize.y, pos.z);
                }
            }

            for (var i = 0; i < height.value; i++)
            {
                for (var j = 0; j < width.value; j++)
                {
                    var obj = Instantiate(placePoint);
                    points[available] = obj;
                    obj.SetActive(true);
                    obj.name = available + "";
                    var tr = obj.transform;
                    tr.SetParent(gameObject.transform);
                    tr = obj.transform;
                    tr.localScale = trpp.localScale;
                    var p = tr.up * grid[i, j].y;
                    tr.localPosition = p + (tr.right * grid[i, j].x);
                    tr.rotation = trpp.rotation;
                    available++;
                }
            }

            var a = 0;
            indPos = new int[width.value * height.value];

            for (var i = available - width.value; i >= 0; i -= width.value)
            {
                for (var j = 0; j < width.value; j++)
                {
                    indPos[a] = i + j;
                    a++;
                }
            }
        }

        private void ScaleBoard()
        {
            var y = (height.value - 1) * scale.y;
            var x = (width.value - 1) * scale.x;

            board.transform.localScale = new Vector3(scale.x + x, scale.y + y, scale.z);
        }

        private void SetUpPieces()
        {
            IndexArray();
            tex = selectedImage.currentSelected;
            av = 0;
            if (pieces != null)
            {
                foreach (var piece in pieces)
                {
                    Destroy(piece);
                }
            }

            quads = new Quad[height.value * width.value];
            rearrangedQuads = new Quad[height.value * width.value];
            var k = 0;
            pieces = new GameObject[height.value * width.value];
            var ppcb = tex.width / width.value;
            var ppch = tex.height / height.value;
            var col = 0;

            for (var i = 0; i < height.value; i++)
            {
                var row = 0;
                for (int j = 0; j < width.value; j++)
                {
                    var q = new Quad();
                    q.rows = new int[ppch, ppcb];
                    quads[k] = q;
                    var p = ppch * tex.width * col + row * ppcb;
                    for (var l = 0; l < ppch; l++)
                    {
                        var x = p + l * tex.width;
                        for (var m = 0; m < ppcb; m++)
                        {
                            q.rows[l, m] = x + m;
                        }
                    }

                    row++;
                    k++;

                    var rect = new Rect(j * (tex.width / width.value), i * (tex.height / height.value),
                        tex.width / width.value, tex.height / height.value);
                    var piece = Instantiate(puzzlePiece);
                    piece.name = av + "";
                    pieces[av] = piece;
                    av++;
                    var tr = piece.transform;
                    tr.parent = gameObject.transform;
                    tr.position = new Vector3(0, 1.1f + (j) * 0.1f, 0.25f - i * 0.15f);
                    tr.localScale = puzzlePiece.transform.localScale;
                    piece.SetActive(true);


                    var sliced = new Texture2D((int)rect.width, (int)rect.height)
                    {
                        filterMode = tex.filterMode
                    };
                    sliced.SetPixels(0, 0, (int)rect.width, (int)rect.height,
                        tex.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
                    sliced.Apply();
                    piece.GetComponent<Renderer>().materials[0].SetTexture(BaseMap, sliced);
                }

                col++;
            }
        }

        private void ShuffleIndex()
        {
            var pos = Int32.Parse(placed.number);
            var point = points[pos];
            var child = point.GetComponent<PlacePoint>().GetPlacedObject();
            var pieceQuad = Int32.Parse(child.name);
            var quad = indPos[pos];
            rearrangedQuads[quad] = quads[pieceQuad];
        }

        public void CompareImage()
        {
            if (placed.amount != (height.value * width.value))
            {
                ShuffleIndex();
                return;
            }

            ShuffleIndex();

            var quadsFlat = new int[tex.height * tex.width];
            var rQuadsFlat = new int[tex.height * tex.width];
            var pos = 0;

            for (var i = 0; i < quads.Length; i++)
            {
                for (var j = 0; j < tex.height / height.value; j++)
                {
                    for (var l = 0; l < tex.width / width.value; l++)
                    {
                        quadsFlat[pos] = quads[i].rows[j, l];
                        rQuadsFlat[pos] = rearrangedQuads[i].rows[j, l];
                        pos++;
                    }
                }
            }

            squares.SetData(quadsFlat);
            rearrangedSquares.SetData(rQuadsFlat);
            var result = new int[tex.width * tex.height];
            buffer.SetData(result);

            comp.SetTexture(0, Image, tex);
            comp.SetBuffer(0, Result, buffer);
            comp.SetBuffer(0, Quads, squares);
            comp.SetBuffer(0, RearrangedQuads, rearrangedSquares);

            comp.SetInt(Height, tex.height);
            comp.SetInt(Width, tex.width);

            var k = new int();
            comp.GetKernelThreadGroupSizes(k, out var x, out var y, out var z);
            comp.Dispatch(k, Mathf.CeilToInt((float)tex.width * tex.height / x), (int)y, (int)z);

            buffer.GetData(result);

            if (result.Any(t => t == 0))
            {
                state.value = false;
                completed.Invoke();
                return;
            }

            state.value = true;
            completed.Invoke();
        }

        private void GoalSprite()
        {
            tex = selectedImage.currentSelected;
            var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));
            goalSprite.image = sprite;
            changedImage.Invoke();
        }

        [Tooltip("width,height")]
        public void SetParameters(string val)
        {
            var s = val.Split(',');
            width.value = Int32.Parse(s[0]);
            height.value = Int32.Parse(s[1]);
            SetUp();
        }
    }
}