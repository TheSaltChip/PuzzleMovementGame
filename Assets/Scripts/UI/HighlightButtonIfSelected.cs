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
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class HighlightButtonIfSelected : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private string id;
        [SerializeField] private bool startSelected;

        public UnityEvent<string> onCheckState;

        private Color _normalColor;
        private Color _selectedColor;

        private void Awake()
        {
            _normalColor = button.colors.normalColor;
            _selectedColor = button.colors.selectedColor;

            if (!startSelected)
                return;

            var cb = button.colors;

            cb.normalColor = _selectedColor;

            button.colors = cb;
        }

        public void Check()
        {
            onCheckState?.Invoke(id);
        }

        public void CheckState(string idName)
        {
            var cb = button.colors;

            cb.normalColor = _normalColor;

            if (id == idName)
            {
                cb.normalColor = _selectedColor;
            }

            button.colors = cb;
        }

        public void Deselect()
        {
            var cb = button.colors;
            cb.normalColor = _normalColor;
            button.colors = cb;
        }

        public void SetId(string newId)
        {
            id = newId;
        }
    }
}