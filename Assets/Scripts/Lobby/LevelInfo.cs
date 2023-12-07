using System.Collections;
using System.Collections.Generic;
using Lobby;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    [SerializeField] private SceneInfo sceneInfo;
    public void DisplayInfo()
    {
        var info = gameObject.GetComponent<TMP_Text>();
        info.text = sceneInfo.sceneDescription;
    }

    public void Localize()
    {
        gameObject.GetComponent<LocalizeStringEvent>().SetEntry(sceneInfo.sceneName);
    }
}
