using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using FloatVariable = Variables.FloatVariable;

namespace Puzzle
{
    public class FloatToSO : MonoBehaviour
    {
        [SerializeField] private FloatVariable so;
        public UnityEvent onChange;

        public void Change(float f)
        {
            so.value = f;
            onChange.Invoke();
        }
    }
}