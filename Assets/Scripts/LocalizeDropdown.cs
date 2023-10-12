using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.Localization
{
    public class LocalizeDropdown : MonoBehaviour
    {
        public List<LocalizedDropdownOption> options;
        [SerializeField] private TMP_Dropdown dropdown;

        private void Start()
        {
            dropdown.ClearOptions();
            
            var snap = options[0].text.GetLocalizedString();
            var cont = options[1].text.GetLocalizedString();
            
            dropdown.AddOptions(new List<string>() {snap});
            dropdown.AddOptions(new List<string>() {cont});
        }

        public void updateLocal()
        {
            dropdown.ClearOptions();
            
            var snap = options[0].text.GetLocalizedString();
            var cont = options[1].text.GetLocalizedString();
            
            dropdown.AddOptions(new List<string>() {snap});
            dropdown.AddOptions(new List<string>() {cont});
        }
    }
}