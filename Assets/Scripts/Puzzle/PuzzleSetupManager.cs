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
        [SerializeField] private PuzzleBoardDimensions boardDimensions;
        [SerializeField] private BoolVariable state;
        
        [SerializeField] private GameObject board;
        [SerializeField] private GameObject placePoint;
        [SerializeField] private Placed placed;

        public UnityEvent SpawnedPieces;
        public UnityEvent completed;

        private Vector3 _scale;
        private Vector3 _placePointSize;
        private GameObject[] _points;
        private GameObject[] _pieces;
        private int _av;
        private Vector3 _boardSize;

        private int _prevPlaced;
        private int[] _indPos;
        private bool[] _correct;

        private void Awake()
        {
            _placePointSize = placePoint.GetComponent<Renderer>().bounds.size;
            _boardSize = board.GetComponent<Renderer>().bounds.size;
            _boardSize.y = _boardSize.x;
            _placePointSize.y = _placePointSize.x;
            _placePointSize *= 14.1f;
            _scale = board.transform.localScale;
            placed.amount = 0;
        }

        public void SetUp()
        {
            ScaleBoard();
            PlacePoints();
            SpawnedPieces.Invoke();
        }

        private void ScaleBoard()
        {
            board.transform.localScale = new Vector3(
                boardDimensions.Width * _scale.x,
                boardDimensions.Height * _scale.y,
                _scale.z);
        }

        private void PlacePoints()
        {
            if (_points != null)
            {
                foreach (var point in _points)
                {
                    Destroy(point);
                }
            }

            var boardHeight = boardDimensions.Height;
            var boardWidth = boardDimensions.Width;

            _points = new GameObject[boardHeight * boardWidth];

            var trpp = placePoint.transform; //transform place point
            var grid = new Vector3[boardHeight, boardWidth];

            var originVectorX = -_placePointSize.x * (boardWidth / 2) + (1 - boardWidth % 2) * _placePointSize.x / 2f;
            var originVectorY = -_placePointSize.y * (boardHeight / 2) + (1 - boardHeight % 2) * _placePointSize.y / 2f;

            for (var i = 0; i < boardHeight; i++)
            {
                for (var j = 0; j < boardWidth; j++)
                {
                    grid[i, j] = new Vector3(
                        originVectorX + j * _placePointSize.x,
                        originVectorY + i * _placePointSize.y,
                        trpp.localPosition.z);
                }
            }

            var available = 0;
            _indPos = new int[boardHeight * boardWidth];
            _correct = new bool[boardHeight * boardWidth];

            for (var i = 0; i < boardHeight; i++)
            {
                for (var j = 0; j < boardWidth; j++)
                {
                    var obj = Instantiate(placePoint);
                    _points[available] = obj;
                    obj.SetActive(true);
                    obj.name = available.ToString();

                    var tr = obj.transform;
                    tr.SetParent(gameObject.transform);
                    tr = obj.transform;

                    var s = Quaternion.AngleAxis(-60, Vector3.forward) * tr.up;

                    var p = s * grid[i, j].y;
                    tr.localPosition = p + tr.right * grid[i, j].x;
                    tr.rotation = trpp.rotation;
                    
                    _indPos[available] = boardWidth * (boardHeight-i-1) + j;
                    
                    available++;
                }
            }
        }

        private void CheckPiecePos()
        {
            var pos = placed.number;
            var point = _points[pos];
            var pointN = _indPos[pos];
            var child = point.GetComponent<PlacePoint>().GetPlacedObject();
            var pieceN = int.Parse(child.name);
            _correct[pieceN] = pointN == pieceN;
        }

        public void CompareImage()
        {
            CheckPiecePos();

            var height = boardDimensions.Height;
            var width = boardDimensions.Width;

            if (placed.amount != height * width)
            {
                return;
            }
            
            if (_correct.Any(val => val != true))
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