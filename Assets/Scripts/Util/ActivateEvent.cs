using UnityEngine;
using UnityEngine.Events;

namespace Util
{
    public class ActivateEvent : MonoBehaviour
    {
        [SerializeField] private bool value;
        
        public UnityEvent activate;

        private void Update()
        {
            if (!value) return;
            
            value = false;
            activate.Invoke();
        }
    }
}