using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class AddSliderValue : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private float scaleValue;

        public UnityEvent<float> OnValueAdded;

        private void Start()
        {
            slider.onValueChanged.AddListener(AddValue);
        }

        private void OnDisable()
        {
            slider.onValueChanged?.RemoveListener(AddValue);
        }

        private void AddValue(float input)
        {
            OnValueAdded.Invoke(Mathf.CeilToInt(input + (input - slider.minValue) * (scaleValue - 1)));
        }
    }
}