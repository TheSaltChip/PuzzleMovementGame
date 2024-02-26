using UnityEngine;
using UnityEngine.Localization.Components;

namespace Lobby
{
    public class SetTableEntryFromSceneInfo : MonoBehaviour
    {
        [SerializeField] private SceneInfo info;
        [SerializeField] private LocalizeStringEvent text;

        public void SetName()
        {
            text.StringReference.TableEntryReference = info.sceneName;
        }
    }
}