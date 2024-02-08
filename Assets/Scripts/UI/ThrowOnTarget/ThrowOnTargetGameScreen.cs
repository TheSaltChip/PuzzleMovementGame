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