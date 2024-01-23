using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace FigureMatching
{
    public class SpawnablePad : MonoBehaviour
    {
        [SerializeField] private BoolVariable canSpawn;

        public UnityEvent enteredPad;
        public UnityEvent exitedPad;

        private void Awake()
        {
            canSpawn.value = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            enteredPad?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            exitedPad?.Invoke();
            
        }
    }
}