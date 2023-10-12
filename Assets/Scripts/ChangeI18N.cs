using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class ChangeI18N : MonoBehaviour
{
    private Toggle _active;
    [SerializeField] private GameObject toggle;

    private void Start()
    {
        _active = toggle.GetComponent<Toggle>();
    }

    public void ChangeLanguage()
    {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
        LocalizationSettings.SelectedLocale = _active.isOn
            ? LocalizationSettings.AvailableLocales.Locales[0]
            : LocalizationSettings.AvailableLocales.Locales[1];
    }
}