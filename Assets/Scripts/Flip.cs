using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Flip : MonoBehaviour
    {
        [SerializeField] private Quaternion rotation;
        private Quaternion _origin;

        private void Start()
        {
            _origin = transform.rotation;
        }

        private void OnCollisionEnter(Collision other)
        {
            transform.rotation = rotation;
            Debug.Log(transform.rotation + " vs " + rotation);
        }
    }
}