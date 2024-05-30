#region License
// Copyright (C) 2024 Sebastian Misje Jonassen & Mathias Nupen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Commons Clause License version 1.0 with GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Commons Clause License and GNU General Public License for more details.
// 
// You should have received a copy of the Commons Clause License and GNU General Public License
// along with this program.  If not, see <https://commonsclause.com/> and <https://www.gnu.org/licenses/>.
#endregion

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