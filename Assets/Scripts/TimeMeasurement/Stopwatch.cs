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

using UnityEngine;
using Variables;

namespace TimeMeasurement
{
    public class Stopwatch : MonoBehaviour
    {
        [SerializeField] private FloatVariable timeSpent;
        private bool _startTime;

        public void ResetTime()
        {
            timeSpent.value = 0;
            _startTime = false;
        }
        
        public void StartTime()
        {
            _startTime = true;
        }

        public void StopTime()
        {
            _startTime = false;
        }

        public void RestartTime()
        {
            timeSpent.value = 0;
            _startTime = true;
        }

        private void Update()
        {
            if (!_startTime) return;

            timeSpent.value += Time.deltaTime;
        }
    }
}