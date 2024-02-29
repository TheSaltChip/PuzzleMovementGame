using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace I18N
{
    public class ChangeI18N : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private SystemLanguage systemLanguage;

        private Locale _english;
        private Locale _norwegian;

        private void Start()
        {
            _english = LocalizationSettings.AvailableLocales.GetLocale(new LocaleIdentifier(SystemLanguage.English));
            _norwegian =
                LocalizationSettings.AvailableLocales.GetLocale(new LocaleIdentifier(SystemLanguage.Norwegian));

            toggle.SetIsOnWithoutNotify(LocalizationSettings.SelectedLocale ==
                                        LocalizationSettings.AvailableLocales.GetLocale(
                                            new LocaleIdentifier(systemLanguage)));
        }

        public void ChangeLanguage()
        {
            LocalizationSettings.SelectedLocale = toggle.isOn ? _norwegian : _english;
        }

        public void SetEnglish()
        {
            LocalizationSettings.SelectedLocale = _english;
        }

        public void SetNorwegian()
        {
            LocalizationSettings.SelectedLocale = _norwegian;
        }
    }
}