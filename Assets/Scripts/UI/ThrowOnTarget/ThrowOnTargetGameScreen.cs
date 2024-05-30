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

using System;
using UnityEngine;
using UnityEngine.Localization.Components;
using Variables;

namespace UI.ThrowOnTarget
{
    public class ThrowOnTargetGameScreen : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent localizeStringEvent;
        [SerializeField] private IntVariable targetsLeftInLevel;
        [SerializeField] private FloatVariable timeSpent;

        [Serializable]
        private class PatRecArguments
        {
            public int TargetsLeftInLevel;
            public string TimeSpent;
        }

        private PatRecArguments _arguments;

        private void Awake()
        {
            _arguments = new PatRecArguments
            {
                TargetsLeftInLevel = targetsLeftInLevel.value,
                TimeSpent = TimeSpan.FromSeconds(timeSpent.value).ToString(@"mm\:ss\.ff")
            };

            localizeStringEvent.StringReference.Arguments = new object[]
            {
                _arguments
            };
        }

        public void SetupString()
        {
            _arguments.TargetsLeftInLevel = targetsLeftInLevel.value;
            _arguments.TimeSpent = TimeSpan.FromSeconds(timeSpent.value).ToString(@"mm\:ss\.ff");
            localizeStringEvent.RefreshString();
        }

        public void UpdateString()
        {
            _arguments.TargetsLeftInLevel = targetsLeftInLevel.value;
            localizeStringEvent.RefreshString();
        }

        private void Update()
        {
            _arguments.TimeSpent = TimeSpan.FromSeconds(timeSpent.value).ToString(@"mm\:ss\.ff");
            localizeStringEvent.RefreshString();
        }
    }
}