using UnityEngine;

namespace Lobby
{
    public class sceneSaveable : MonoBehaviour
    {
        public string name;
        public string description;
        
        public override string ToString()
        {
            return $"{nameof(name)}: {name}, {nameof(description)}: {description}";
        }
    }
}