using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SceneTransition;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UI
{
    public class WristMenu : MonoBehaviour
    {
        [SerializeField] private GameObject canvas;
        [SerializeField] private TMP_Dropdown dropdown;

        public UnityEvent<string> changeScene;
        
        private void Start()
        {
            ListScenes();
        }

        public void OpenCloseMenu()
        {
            canvas.SetActive(!canvas.activeSelf);
        }

        public void ChangeScene()
        {
            changeScene?.Invoke(dropdown.options[dropdown.value].text);
        }

        private void ListScenes()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var optionDataList = new List<TMP_Dropdown.OptionData>();

            for (var i = 1; i < sceneCount; i++)
            {
                var sceneName = SceneUtility.GetScenePathByBuildIndex(i)
                    .Split('/')
                    .Last()
                    .Split('.')[0];
            
                optionDataList.Add(new TMP_Dropdown.OptionData(sceneName));
            }

            dropdown.options = optionDataList;
        }
    }
}