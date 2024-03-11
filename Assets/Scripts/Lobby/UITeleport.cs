using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Lobby
{
    public class UITeleport : MonoBehaviour
    {
        [SerializeField] private bool staticGate;

        [SerializeField, HideIf("staticGate")] private SceneInfo sceneInfo;
        [SerializeField, ShowIf("staticGate")] private StringVariable targetSceneName;

        public UnityEvent<string> sceneNameToTeleportTo;

        public void ChangeScene()
        {
            sceneNameToTeleportTo?.Invoke(staticGate ? targetSceneName.Value : sceneInfo.sceneName);
        }
    }
}
