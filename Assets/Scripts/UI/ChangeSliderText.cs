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
        [SerializeField, Tooltip("text.text = (inputText * constant).ToString(CultureInfo.InvariantCulture);")] private float constant = 1f;

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
            text.text = (inputText * constant)
                .ToString(CultureInfo.InvariantCulture);
        }
    }
}