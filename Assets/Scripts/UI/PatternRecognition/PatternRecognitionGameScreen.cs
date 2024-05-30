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
using PatternRecognition.ScriptableObjects;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace UI.PatternRecognition
{
    public class PatternRecognitionGameScreen : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent localizeStringEvent;
        [SerializeField] private PatternRecognitionRules rules;

        [Serializable]
        private class PatRecArguments
        {
            public int currentPatternLength;
            public int buttonsLeft;
            public int bestScore;
            public bool continuousMode;
        }

        private PatRecArguments _arguments;

        private void Awake()
        {
            _arguments = new PatRecArguments
            {
                currentPatternLength = rules.PatternLength,
                buttonsLeft = rules.PatternLength - rules.PatternIndex,
                bestScore = rules.BestScore,
                continuousMode = rules.ContinuousMode
            };

            localizeStringEvent.StringReference.Arguments = new object[]
            {
                _arguments
            };
        }

        public void SetupString()
        {
            _arguments.currentPatternLength = rules.PatternLength;
            _arguments.buttonsLeft = rules.PatternLength - rules.PatternIndex;
            _arguments.bestScore = rules.BestScore;
            _arguments.continuousMode = rules.ContinuousMode;

            localizeStringEvent.RefreshString();
        }

        public void UpdateString()
        {
            _arguments.buttonsLeft = rules.PatternLength - rules.PatternIndex;
            _arguments.bestScore = rules.BestScore;
            _arguments.continuousMode = rules.ContinuousMode;

            localizeStringEvent.RefreshString();
        }

        public void UpdatePatternLength()
        {
            _arguments.currentPatternLength = rules.PatternLength;
            _arguments.buttonsLeft = rules.PatternLength - rules.PatternIndex;
            localizeStringEvent.RefreshString();
        }
    }
}