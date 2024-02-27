using System;
using UnityEngine;

namespace XR
{
    public class SetStart : MonoBehaviour
    {
        private void Start()
        {
            var g = GameObject.FindWithTag("SpawnPoint");

            transform.position = g.transform.position;
            transform.rotation = g.transform.rotation;
        }
    }
}