using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Utilities.Localization
{
    public class LocalizeDropdown : MonoBehaviour
    {
        public List<LocalizedDropdownOption> options;
        [SerializeField] private TMP_Dropdown dropdown;

        private List<string> _list;

        private void Awake()
        {
            _list = new List<string>();
        }

        private void Start()
        {
            UpdateOptions();
        }

        public void UpdateOptions()
        {
            var selected = dropdown.value;
            dropdown.ClearOptions();

            _list.Clear();
            
            foreach (var option in options)
            {
                _list.Add(option.text.GetLocalizedString());
            }

            dropdown.AddOptions(_list);
            dropdown.SetValueWithoutNotify(selected);
        }
    }
}