using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Puzzle
{
    public class FloatToIntSO : MonoBehaviour
    {
        [SerializeField] private IntVariable so;
        public UnityEvent onChange;

        public void Change(float f)
        {
            so.value = (int)f;
            onChange.Invoke();
        }
    }
}