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

using System.Globalization;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace UI
{
    public class ChangeSliderText : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text text;

        [SerializeField, Tooltip("text.text = (inputText * constant).ToString(format, CultureInfo.InvariantCulture);")]
        private float constant = 1f;

        [SerializeField, Tooltip("text.text = (inputText * constant).ToString(format, CultureInfo.InvariantCulture);")]
        private string format = "";

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(ChangeText);
            ChangeText(slider.value);
        }

        private void OnDisable()
        {
            slider.onValueChanged?.RemoveListener(ChangeText);
        }

        private void ChangeText(float inputText)
        {
            text.text = (inputText * constant).ToString(format, CultureInfo.InvariantCulture);
        }

        public void SetTextFromSlider()
        {
            text.text = (slider.value * constant).ToString(format, CultureInfo.InvariantCulture);
        }
    }
}