using UnityEngine;

namespace Variables
{
    [CreateAssetMenu(menuName = "Variables/StringVariable", fileName = "StringVariable")]
    public class StringVariable : ScriptableObject
    {
        public string value;
    }
}