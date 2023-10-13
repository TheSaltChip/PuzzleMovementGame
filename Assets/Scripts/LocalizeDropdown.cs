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
            updateOptions();
        }

        public void updateOptions()
        {
            var selected = dropdown.value;
            dropdown.ClearOptions();

            foreach (var option in options)
            {
                var str = option.text.GetLocalizedString();
                dropdown.AddOptions(new List<string>() {str});
            }
            
            dropdown.SetValueWithoutNotify(selected);
        }
    }
}