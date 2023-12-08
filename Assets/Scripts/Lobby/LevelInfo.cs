using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Serialization;

namespace Lobby
{
    public class LevelInfo : MonoBehaviour
    {
        [SerializeField] private SceneInfo sceneInfo;
        [SerializeField] private LocalizeStringEvent localizeStringEvent;
        [SerializeField] private TMP_Text sceneDescription;

        public void DisplayInfo()
        {
            sceneDescription.text = sceneInfo.sceneDescription;
        }

        public void Localize()
        {
            localizeStringEvent.SetEntry(sceneInfo.sceneName);
        }
    }
}