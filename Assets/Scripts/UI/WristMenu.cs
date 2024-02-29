using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        [SerializeField] private TMP_Text debugText;

        private Dictionary<string, string> debugLogs = new();

        public UnityEvent<string> changeScene;

        private void OnEnable()
        {
            Application.logMessageReceived += HandleException;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleException;
        }

        private void HandleException(string condition, string stacktrace, LogType type)
        {
            if (type is LogType.Exception)
            {
                var splitString = condition.Split(':');
                var debugKey = splitString[0];
                var debugValue = splitString.Length > 1 ? splitString[1] + "\n" + stacktrace : "";

                debugLogs[debugKey] = debugValue;
            }

            if (type is LogType.Log)
            {
                var splitString = condition.Split(':');
                var debugKey = splitString[0];
                var debugValue = splitString.Length > 1 ? splitString[1] : "";

                debugLogs[debugKey] = debugValue;
            }

            var sb = new StringBuilder();

            foreach (var log in debugLogs)
            {
                sb.AppendLine(log.Value == "" ? log.Key : $"{log.Key}: {log.Value}");
            }

            debugText.text = sb.ToString();
        }

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

            for (var i = 0; i < sceneCount; i++)
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