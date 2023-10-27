using System.Globalization;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class ChangeSliderText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float constant = 1f;
        
        public Slider Slider { get; private set; }

        private void Awake()
        {
            if (!gameObject.TryGetComponent(out Slider slider)) return;
            
            Slider = slider;

            Slider.onValueChanged.AddListener(ChangeText);
        }

        public void ChangeText(float inputText)
        {
            text.text = (inputText * constant).ToString(CultureInfo.InvariantCulture);
        }
    }
}