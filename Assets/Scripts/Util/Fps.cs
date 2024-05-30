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

namespace Util
{
    public class Fps : MonoBehaviour
    {
        private float count;
        private WaitForSeconds _waitForSeconds;
        [SerializeField] private TMP_Text text;

        private IEnumerator Start()
        {
            _waitForSeconds = new WaitForSeconds(0.1f);
            while (true)
            {
                count = 1f / Time.unscaledDeltaTime;
                yield return _waitForSeconds;
            }
        }

        private void OnGUI()
        {
            text.text = $"FPS: {Mathf.Round(count)}";
        }
    }
}