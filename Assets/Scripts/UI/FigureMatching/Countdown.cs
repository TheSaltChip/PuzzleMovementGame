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

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace UI.FigureMatching
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField, Min(0)] private int startNumber;
        [SerializeField] private BoolVariable canStartCountdown;

        private WaitForSeconds _oneSecond;
        private int _num;

        public UnityEvent tick;
        public UnityEvent countdownCompleted;

        private void Awake()
        {
            _oneSecond = new WaitForSeconds(1f);
        }

        public void StartCountdown()
        {
            _num = startNumber;
            text.text = _num.ToString();
            StartCoroutine(StartCountdownCoroutine());
        }

        private IEnumerator StartCountdownCoroutine()
        {
            yield return new WaitUntil(() => canStartCountdown.value);
            
            while (_num > 0)
            {
                tick?.Invoke();
                yield return _oneSecond;
                --_num;
                text.text = _num.ToString();
            }

            countdownCompleted?.Invoke();
        }
    }
}