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
using System.Globalization;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using UnityEngine.Localization.Components;
using Variables;

namespace UI.ThrowOnTarget
{
    public class ThrowOnTargetEndScreen : MonoBehaviour
    {
        [SerializeField] private LocalizeStringEvent localizeStringEvent;
        [SerializeField] private ThrowLevelRules rules;
        [SerializeField] private FloatVariable timeSpent;

        [Serializable]
        private class Arguments
        {
            public int NumTargets;
            public int NumStages;
            public string DistBetweenStages;
            public string ChanceForBigTarget;
            public string TimeSpent;
        }

        private Arguments _arguments;

        // TODO - Make it so chance for big target displays string rep instead of actual chance
        private void Start()
        {
            _arguments = new Arguments
            {
                NumTargets = rules.TargetsPerStage,
                NumStages = rules.Stages,
                DistBetweenStages = $"{rules.DistBetweenStages.ToString("##.#", CultureInfo.InvariantCulture)}m",
                ChanceForBigTarget = rules.ChanceBigTarget.ToString("##0%", CultureInfo.InvariantCulture),
                TimeSpent = TimeSpan.FromSeconds(timeSpent.value).ToString(@"mm\:ss\.ff")
            };

            localizeStringEvent.StringReference.Arguments = new object[]
                { _arguments };
        }

        public void LevelCompleted()
        {
            _arguments.NumTargets = rules.TargetsPerStage;
            _arguments.NumStages = rules.Stages;
            _arguments.DistBetweenStages = $"{rules.DistBetweenStages.ToString("##.#", CultureInfo.InvariantCulture)}m";
            _arguments.ChanceForBigTarget = rules.ChanceBigTarget.ToString("##0%", CultureInfo.InvariantCulture);
            _arguments.TimeSpent = TimeSpan.FromSeconds(timeSpent.value).ToString(@"mm\:ss\.ff");

            localizeStringEvent.RefreshString();
        }
    }
}