using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(menuName = "Variables/BoolVariable")]
    public class BoolVariable : ScriptableObject
    {
        public bool value;

        public void Set(bool b)
        {
            value = b;
        }
    }
}