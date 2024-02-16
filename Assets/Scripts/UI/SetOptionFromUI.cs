using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SetOptionFromUI : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        
        private void Start()
        {
            volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        }

        public void SetGlobalVolume(float value)
        {
            AudioListener.volume = value;
        }
    }
}