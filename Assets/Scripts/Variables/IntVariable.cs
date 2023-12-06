using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(menuName = "Variables/IntVariable")]
    public class IntVariable : ScriptableObject
    {
        public int value;
        
        public void Increment()
        {
            ++value;
        }

        public void Decrement()
        {
            --value;
        }
    }
}