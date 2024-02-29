using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace I18N
{
    public class SetToggleFromLanguage : MonoBehaviour
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private SystemLanguage systemLanguage;

        private void Start()
        {
            Set();
        }

        public void Set()
        {
            toggle.SetIsOnWithoutNotify(LocalizationSettings.SelectedLocale ==
                                        LocalizationSettings.AvailableLocales.GetLocale(
                                            new LocaleIdentifier(systemLanguage)));
        }
    }
}