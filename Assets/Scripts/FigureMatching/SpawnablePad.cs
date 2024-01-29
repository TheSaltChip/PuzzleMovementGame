using System;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace FigureMatching
{
    public class SpawnablePad : MonoBehaviour
    {
        [SerializeField] private BoolVariable canSpawn;
        [SerializeField] private BoolVariable gameStarted;

        public UnityEvent enteredPad;
        public UnityEvent staysOnPad;
        public UnityEvent exitedPad;

        private void Awake()
        {
            canSpawn.value = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            enteredPad?.Invoke();
        }

        private void OnTriggerStay(Collider other)
        {
            staysOnPad?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (gameStarted.value) 
                return;
            
            exitedPad?.Invoke();
        }
    }
}