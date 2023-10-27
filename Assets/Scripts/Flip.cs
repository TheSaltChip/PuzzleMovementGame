using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Flip : MonoBehaviour
    {
        [SerializeField] private Quaternion rotation;
        private Quaternion _origin;
        private int _rotated;
        private bool rotated;

        private void Start()
        {
            _origin = transform.rotation;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_rotated < 2 && !rotated)
            {
                transform.rotation = rotation;
                rotated = true;
                _rotated++;
                BroadcastMessage("Flip");
            }
        }

        public void Reset()
        {
            _rotated = 0;
            transform.rotation = _origin;
            rotated = false;
        }
    }
}