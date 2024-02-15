using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class FloatToInt : MonoBehaviour
    {
        [SerializeField, Tooltip("Mathf.RoundToInt(value) * factor")] private int factor = 1;
        
        public UnityEvent<int> onConverted;

        public void Convert(float value)
        {
            onConverted?.Invoke(Mathf.RoundToInt(value) * factor);
        }
    }
}