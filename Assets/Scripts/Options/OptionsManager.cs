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

using Autohand;
using Constants;
using UnityEngine;
using UnityEngine.Events;

namespace Options
{
    public class OptionsManager : MonoBehaviour
    {
        [SerializeField] private GameOptions gameOptions;

        public UnityEvent onOptionsChanged;

        private void Start()
        {
            gameOptions.SnapTurnAngle = AutoHandPlayer.Instance.snapTurnAngle;
            gameOptions.SmoothTurnSpeed = AutoHandPlayer.Instance.smoothTurnSpeed;
            gameOptions.RotationType = (int)AutoHandPlayer.Instance.rotationType;

            if (PlayerPrefs.HasKey(PlayerPrefsNames.Turn))
            {
                var rotType = PlayerPrefs.GetInt(PlayerPrefsNames.Turn);
                gameOptions.RotationType = rotType;
            }

            if (PlayerPrefs.HasKey(PlayerPrefsNames.TurnSpeed))
            {
                var smoothTurnSpeed = PlayerPrefs.GetFloat(PlayerPrefsNames.TurnSpeed);
                gameOptions.SmoothTurnSpeed = smoothTurnSpeed;
            }

            if (PlayerPrefs.HasKey(PlayerPrefsNames.SnapTurnAngle))
            {
                var snapTurnAngle = PlayerPrefs.GetFloat(PlayerPrefsNames.SnapTurnAngle);
                gameOptions.SnapTurnAngle = snapTurnAngle;
            }

            onOptionsChanged?.Invoke();
        }

        public void SetTurnOption(int value)
        {
            PlayerPrefs.SetInt(PlayerPrefsNames.Turn, value);
            gameOptions.RotationType = value;

            SaveOptions();
        }

        public void SetTurnSpeed(float smoothTurnSpeed)
        {
            PlayerPrefs.SetFloat(PlayerPrefsNames.TurnSpeed, smoothTurnSpeed);
            gameOptions.SmoothTurnSpeed = smoothTurnSpeed;
            SaveOptions();
        }

        public void SetSnapTurnAngle(float snapTurnAngle)
        {
            PlayerPrefs.SetFloat(PlayerPrefsNames.SnapTurnAngle, snapTurnAngle);
            gameOptions.SnapTurnAngle = snapTurnAngle;
            SaveOptions();
        }

        private void SaveOptions()
        {
            PlayerPrefs.Save();
            onOptionsChanged?.Invoke();
        }
    }
}