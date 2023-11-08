using System;
using System.Collections;
using Unity.XR.CoreUtils.Bindings.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Completables.ColorRecognition
{
    public class ColorRecognitionBoardEditor : MonoBehaviour
    {
        private const int GridMax = 5;

        [SerializeField] private GameObject boardPlate;
        [SerializeField] private int gridDimensionX = 1;
        [SerializeField] private int gridDimensionZ = 1;
        [SerializeField] private GameObject buttonPrefab;

        [SerializeField] private Rigidbody baseRigidbody;
        [SerializeField] private BoxCollider poseCollider;

        public UnityEvent beforeResize;
        public UnityEvent afterResize;


        private readonly float _buttonScale = 0.05f;
        private readonly float _padding = 0.0125f;

        private GameObject[] _buttonsPool;

        // Base is 0.2 x 0.2 for 3x3
        // 0.05 * 3 = 0.15 => 0.0125 * (num buttons + 1) padding = 0.0125 * 4 = 0.05
        // Base is 0.x x 0.265 for 3x4
        // 0.05 * 4 = 0.25 => 0.0125 * (num buttons + 1) padding = 0.0125 * 5 = 0.065

        private Vector3 _gridSize;

        private void Awake()
        {
            _gridSize = new Vector3(gridDimensionX * _buttonScale + (gridDimensionX + 1) * _padding,
                boardPlate.transform.localScale.y,
                gridDimensionZ * _buttonScale + (gridDimensionZ + 1) * _padding);

            _buttonsPool = new GameObject[25];
        }

        private void Start()
        {
            var colSize = poseCollider.size;

            colSize.x = _gridSize.x;
            colSize.z = _gridSize.z;
            poseCollider.size = colSize;

            boardPlate.transform.localScale = _gridSize;

            boardPlate.GetComponent<BoxCollider>().size = _gridSize;

            var x = 0f;
            var z = 0f;
            var dx = 0f;
            var dz = -1f;

            var scale = _buttonScale + _padding;

            const int gridMaxSquared = GridMax * GridMax;
            
            for (var i = 0; i < gridMaxSquared; i++)
            {
                var button = Instantiate(buttonPrefab, transform, true);
                button.SetActive(false);

                var rb = button.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                
                button.transform.SetLocalPositionAndRotation(
                    new Vector3(x * scale, _gridSize.y - 0.005f, z * scale),
                    Quaternion.identity);
                button.GetComponent<ConfigurableJoint>().connectedBody = baseRigidbody;

                rb.isKinematic = false;

                _buttonsPool[i] = button;


                if (Math.Abs(x - z) < 0.001f
                    || (x < 0 && Math.Abs(x - -z) < 0.001f)
                    || (x > 0 && Math.Abs(x - (1 - z)) < 0.001f))
                {
                    (dx, dz) = (-dz, dx);
                }

                x += dx;
                z += dz;
            }

            ScaleBoard();
        }

        public void ScaleBoardX(float x)
        {
            gridDimensionX = Mathf.Clamp(Mathf.RoundToInt(x), 1, GridMax);
            _gridSize.x = gridDimensionX * _buttonScale + (gridDimensionX + 1) * _padding;

            ScaleBoard();
        }

        public void ScaleBoardZ(float z)
        {
            gridDimensionZ = Mathf.Clamp(Mathf.RoundToInt(z), 1, GridMax);
            _gridSize.z = gridDimensionZ * _buttonScale + (gridDimensionZ + 1) * _padding;

            ScaleBoard();
        }

        private void ScaleBoard()
        {
            beforeResize?.Invoke();

            var colSize = poseCollider.size;

            colSize.x = _gridSize.x;
            colSize.z = _gridSize.z;
            poseCollider.size = colSize;

            boardPlate.transform.localScale = _gridSize;

            boardPlate.GetComponent<BoxCollider>().size = _gridSize;

            var x = 0f;
            var z = 0f;
            var dx = 0f;
            var dz = -1f;

            const int gridMaxSquared = GridMax * GridMax;
            var halfDimensionX = gridDimensionX / 2f;
            var halfDimensionZ = gridDimensionZ / 2f;

            for (var i = 0; i < gridMaxSquared; i++)
            {
                _buttonsPool[i].SetActive(-halfDimensionX <= x 
                                          && x <= halfDimensionX 
                                          && -halfDimensionZ <= z 
                                          && z <= halfDimensionZ);

                if (Math.Abs(x - z) < 0.001f
                    || (x < 0 && Math.Abs(x - -z) < 0.001f)
                    || (x > 0 && Math.Abs(x - (1 - z)) < 0.001f))
                {
                    (dx, dz) = (-dz, dx);
                }

                x += dx;
                z += dz;
            }

            afterResize?.Invoke();
        }
    }
}