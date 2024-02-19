using UI.Lobby;
using UnityEngine;

namespace Lobby
{
    public class SelectedLevelSetup : MonoBehaviour
    {
        [SerializeField] private SceneInfo sceneInfo;

        public void SetSceneName()
        {
            sceneInfo.sceneName = gameObject.GetComponentInChildren<SceneName>().sceneName;
        }
    }
}