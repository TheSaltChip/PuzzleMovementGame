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

using System.Linq;
using UI;
using UI.Lobby;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class LevelInfoManager : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;

        private void Awake()
        {
            LoadLevelNames();
        }

        public void LoadLevelNames()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            for (var i = 1; i < sceneCount; i++)
            {
                var sceneName = SceneUtility.GetScenePathByBuildIndex(i)
                    .Split('/')
                    .Last()
                    .Split('.')[0];

                var button = Instantiate(buttonPrefab, gameObject.transform);
                button.SetActive(true);
                var localizeStringEvent = button.GetComponentInChildren<LocalizeStringEvent>();
                localizeStringEvent.SetTable("Lobby");
                localizeStringEvent.SetEntry(sceneName);
                
                var highlightButton = button.GetComponentInChildren<HighlightButtonIfSelected>();
                highlightButton.SetId(i.ToString());

                var hoverActions = button.GetComponentInChildren<LevelButtonHoverActions>();
                hoverActions.SetStringEntry(sceneName + "Info");
                
                var sceneInfo = button.GetComponentInChildren<SceneName>();
                sceneInfo.sceneName = sceneName;
            }
        }
    }
}