using UnityEngine;

namespace Lobby
{
    [CreateAssetMenu(fileName = "SceneInformation", menuName = "Lobby/SceneInformation", order = 0)]
    public class SceneInfo : ScriptableObject
    {
        public string sceneName;

        public string sceneDescription;
    }
}