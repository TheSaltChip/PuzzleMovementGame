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