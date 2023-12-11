using UnityEngine;

namespace Tutorial
{
    public class ControllerLookAtHandler : MonoBehaviour
    {
        [SerializeField] private Vector3 rotationForFace;
        private Quaternion _originalRotation;
        private Transform _tr;
    
        void Start()
        {
            _tr = gameObject.transform;
            _originalRotation = _tr.rotation;
        }

        public void SideView()
        {
            _tr.rotation = _originalRotation;
        }

        public void TopView()
        {
            _tr.rotation = Quaternion.Euler(rotationForFace);
        }

    }
}
