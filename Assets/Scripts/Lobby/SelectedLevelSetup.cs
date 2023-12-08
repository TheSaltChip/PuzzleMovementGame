using TMPro;
using UnityEngine;

namespace Lobby
{
    public class SelectedLevelSetup : MonoBehaviour
    {
        [SerializeField] private SceneInfo sceneInfo;

        // Sets scene information on interactive UI button
        public void SetSceneName()
        {
            sceneInfo.sceneName = gameObject.GetComponentInChildren<TMP_Text>().text;
        }
    }
}