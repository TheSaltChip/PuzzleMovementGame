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

using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Lobby
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private bool staticGate;

        [SerializeField, HideIf("staticGate")] private SceneInfo sceneInfo;
        [SerializeField, ShowIf("staticGate")] private StringVariable targetSceneName;

        private bool _active;
        private bool _collided;

        public UnityEvent<string> sceneNameToTeleportTo;

        private void OnTriggerEnter(Collider other)
        {
            if (_collided || (!_active && !staticGate)) return;
            
            _collided = true;
            sceneNameToTeleportTo?.Invoke(staticGate ? targetSceneName.value : sceneInfo.sceneName);
        }

        public void ActivateGate()
        {
            _active = true;
        }
    }
}