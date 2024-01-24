using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UI.PatternRecognition
{
    public class InvokeEventWithVector2Int : MonoBehaviour
    {
        [SerializeField] private Vector2Int val;

        public UnityEvent<Vector2Int> onEvent;

        public void Invoke()
        {
            onEvent.Invoke(val);
        }
    }
}