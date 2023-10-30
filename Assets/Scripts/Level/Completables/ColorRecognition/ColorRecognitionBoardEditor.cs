using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

namespace Level.Completables.ColorRecognition
{
    public class ColorRecognitionBoardEditor : MonoBehaviour
    {
        [SerializeField] private GameObject boardPlate;
        [SerializeField] private int gridDimensionX = 3;
        [SerializeField] private int gridDimensionZ = 3;
        [SerializeField] private GameObject buttonPrefab;

        [SerializeField] private Rigidbody rb;
        [SerializeField] private BoxCollider poseCollider;


        private float _buttonScale = 0.05f;
        private float _padding = 0.0125f;

        public UnityEvent beforeResize;
        public UnityEvent afterResize;

        private ObjectPool<GameObject> _buttonsPool;

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

            _buttonsPool = new ObjectPool<GameObject>(CreateButton, GetFromPool, OnReleaseToPool, OnDestroyFromPool,
                defaultCapacity: 16);

            ScaleBoard();
        }

        private void OnDestroyFromPool(GameObject obj)
        {
            Destroy(obj);
        }

        private void OnReleaseToPool(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void GetFromPool(GameObject obj)
        {
            obj.SetActive(true);
        }

        private GameObject CreateButton()
        {
            var go = Instantiate(buttonPrefab, transform, true);
            var rtp = go.AddComponent<ReturnToPool>();
            rtp.pool = _buttonsPool;
            beforeResize.AddListener(rtp.Return);
            return go;
        }

        public void ScaleBoardX(float x)
        {
            gridDimensionX = (int)Mathf.Clamp(Mathf.Round(x), 1, 4);
            _gridSize.x = gridDimensionX * _buttonScale + (gridDimensionX + 1) * _padding;

            ScaleBoard();
        }

        public void ScaleBoardZ(float z)
        {
            gridDimensionZ = (int)Mathf.Clamp(Mathf.Round(z), 1, 4);
            _gridSize.z = gridDimensionZ * _buttonScale + (gridDimensionZ + 1) * _padding;

            ScaleBoard();
        }

        private void ScaleBoard()
        {
            beforeResize?.Invoke();

            var colSize = poseCollider.size;
            colSize.z = _gridSize.z;
            colSize.x = _gridSize.x;
            poseCollider.size = colSize;

            boardPlate.transform.localScale = _gridSize;

            boardPlate.GetComponent<BoxCollider>().size = _gridSize;

            var step = _padding + _buttonScale;

            var xLimit = _gridSize.x / 2 - _buttonScale / 2 - _padding;
            var zLimit = _gridSize.z / 2 - _buttonScale / 2 - _padding;

            for (var x = -xLimit; x <= xLimit; x += step)
            {
                for (var z = -zLimit; z <= zLimit; z += step)
                {
                    var button = _buttonsPool.Get();

                    var rb = button.GetComponent<Rigidbody>();
                    rb.isKinematic = true;

                    button.transform.SetLocalPositionAndRotation(new Vector3(x, _gridSize.y - 0.005f, z),
                        Quaternion.identity);

                    button.GetComponent<ConfigurableJoint>().connectedBody = this.rb;

                    rb.isKinematic = false;
                }
            }

            afterResize?.Invoke();
        }
    }

    public class ReturnToPool : MonoBehaviour
    {
        public IObjectPool<GameObject> pool;

        public void Return()
        {
            // Return to the pool

            if (!gameObject.activeSelf) return;

            pool.Release(gameObject);
            GetComponent<ConfigurableJoint>().connectedBody = null;
        }
    }
}