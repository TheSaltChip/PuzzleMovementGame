using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class RaiseFloatWithFactor : MonoBehaviour
    {
        [SerializeField] private float factor;

        public UnityEvent<float> onRaised;

        public void Raise(float value)
        {
            onRaised?.Invoke(value * factor);
        }
    }
}