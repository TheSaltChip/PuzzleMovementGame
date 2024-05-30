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

using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

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