using UnityEngine;

namespace ThrowingOnTargets
{
    public class FollowCameraInXAxis : MonoBehaviour
    {
        private Transform _mainCameraTransform;

        private void Start()
        {
            SetCameraTransform();
        }

        public void SetCameraTransform()
        {
            var mainCamera = GameObject.FindWithTag("MainCamera");
            _mainCameraTransform = mainCamera == null ? transform : mainCamera.transform;
        }

        private void Update()
        {
            var pos = transform.position;
            transform.position = new Vector3(_mainCameraTransform.position.x, pos.y, pos.z);
        }
    }
}