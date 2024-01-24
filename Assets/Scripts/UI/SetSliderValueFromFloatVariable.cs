using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Variables;

namespace UI
{
    public class SetSliderValueFromFloatVariable : MonoBehaviour
    {
        [SerializeField] private FloatVariable variable;
        [SerializeField] private Slider slider;

        private void Awake()
        {
            slider.value = variable.value;
        }
    }
}