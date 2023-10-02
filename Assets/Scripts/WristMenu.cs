using System.Collections;
using System.Collections.Generic;
using SceneTransition;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class WristMenu : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TMP_Dropdown dropdown;

    public void OpenCloseMenu()
    {
        canvas.SetActive(!canvas.activeSelf);
    }

    public void ChangeScene()
    {
        SceneTransitionManager.Instance.LoadScene(dropdown.options[dropdown.value].text);
    }

    void Start()
    {
        ListScenes();
    }

    private void ListScenes()
    {
        var sceneCount = SceneManager.sceneCountInBuildSettings;
        var optionDataList = new List<TMP_Dropdown.OptionData>();

        for (var i = 1; i < sceneCount; i++)
        {
            var sceneName = SceneUtility.GetScenePathByBuildIndex(i);
            optionDataList.Add(new TMP_Dropdown.OptionData(sceneName));
        }

        dropdown.options = optionDataList;
    }
}