using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(menuName = "Variables/FloatVariable")]
    public class FloatVariable : ScriptableObject
    {
        public float value;

        public void Set(float val)
        {
            value = val;
        }
    }
}