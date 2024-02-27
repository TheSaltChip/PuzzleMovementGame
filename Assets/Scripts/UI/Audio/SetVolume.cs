using UnityEngine;
using UnityEngine.UI;

namespace UI.Audio
{
    public class SetVolume : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        private void Start()
        {
            var volume = PlayerPrefs.GetFloat("MasterVolume", 0.50f);
            slider.value = volume;
            AudioListener.volume = volume;
        }
        
        public void SetLevel(float sliderValue)
        {
            AudioListener.volume = sliderValue;
            PlayerPrefs.SetFloat("MasterVolume", sliderValue);
            PlayerPrefs.Save();
        }
    }
}