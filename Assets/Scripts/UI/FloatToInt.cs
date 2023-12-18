using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class FloatToInt : MonoBehaviour
    {
        public UnityEvent<int> onConverted;

        public void Convert(float value)
        {
            onConverted?.Invoke(Mathf.RoundToInt(value));
        }
    }
}