using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

namespace Lobby
{
    public class SOToText : MonoBehaviour
    {
        [SerializeField] private SceneInfo info;
        [SerializeField] private LocalizeStringEvent text;

        public void ShowName()
        {
            var strings = LocalizationSettings.StringDatabase.GetTable("Lobby");
            text.StringReference.TableEntryReference = info.sceneName;
            //text.StringReference.TableEntryReference = strings.SharedData.GetEntry(info.sceneName).Key;
        }
    }
}