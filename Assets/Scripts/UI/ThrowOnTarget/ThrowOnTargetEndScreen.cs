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