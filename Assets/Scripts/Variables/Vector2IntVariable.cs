using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector2IntVariable")]
    public class Vector2IntVariable : ScriptableObject
    {
        public Vector2Int value;

        public void Set(Vector2Int i)
        {
            value = i;
        }
    }
}