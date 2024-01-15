using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using FloatVariable = Variables.FloatVariable;
using IntVariable = Variables.IntVariable;

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