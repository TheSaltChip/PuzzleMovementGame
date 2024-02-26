using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace I18N
{
    public class ChangeI18N : MonoBehaviour
    {
        [SerializeField] private Toggle active;

        public void ChangeLanguage()
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            LocalizationSettings.SelectedLocale = active.isOn
                ? LocalizationSettings.AvailableLocales.Locales[0]
                : LocalizationSettings.AvailableLocales.Locales[1];
        }
    }
}