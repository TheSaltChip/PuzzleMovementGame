using UnityEngine;

namespace Lobby
{
    [CreateAssetMenu(fileName = "NextScene", menuName = "Lobby/NextScene", order = 0)]
    public class NextScene : ScriptableObject
    {
        public string sceneName;
    }
}