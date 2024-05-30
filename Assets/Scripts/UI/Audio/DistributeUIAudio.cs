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
using Events.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Audio
{
    public class DistributeUIAudio : MonoBehaviour
    {
        [SerializeField] private EventAudio eventAudio;
        [SerializeField] private AudioGameEvent audioGameEvent;

        private void Start()
        {
            var gameObjects = new List<GameObject>();
            gameObjects.AddRange(gameObject.GetComponentsInChildren<Button>(true).Select(x => x.gameObject));
            gameObjects.AddRange(gameObject.GetComponentsInChildren<Slider>(true).Select(x => x.gameObject));
            gameObjects.AddRange(gameObject.GetComponentsInChildren<Dropdown>(true).Select(x => x.gameObject));
            gameObjects.AddRange(gameObject.GetComponentsInChildren<Toggle>(true).Select(x => x.gameObject));

            foreach (var item in gameObjects)
            {
                var trigger = item.AddComponent<UIAudio>();
                trigger.EventAudio = eventAudio;
                trigger.AudioGameEvent = audioGameEvent;
            }
        }
    }
}