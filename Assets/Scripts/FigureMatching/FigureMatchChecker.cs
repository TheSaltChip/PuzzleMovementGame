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

using System.Collections.Generic;
using FigureMatching.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace FigureMatching
{
    public class FigureMatchChecker : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;

        private List<FigureInfo> _list;

        public UnityEvent onCorrectMatch;
        public UnityEvent onIncorrectMatch;
        public UnityEvent onAddedToList;

        private void Awake()
        {
            _list = new List<FigureInfo>(5);
        }

        public void Add(FigureInfo item)
        {
            if (_list.Contains(item)) return;

            _list.Add(item);
            onAddedToList?.Invoke();
            item.Deactivate();
            
            if (_list.Count > 1)
            {
                Matches();
            }
        }

        public void Matches()
        {
            var item = _list[0];

            for (var i = 1; i < _list.Count; i++)
            {
                if (!item.Equals(_list[i]))
                {
                    ActivateAndClear();
                    onIncorrectMatch?.Invoke();
                    return;
                }

                item = _list[i];
            }
            
            if(_list.Count < rules.NumToMatch) return;

            DeleteAndClear();
            onCorrectMatch?.Invoke();
        }

        private void DeleteAndClear()
        {
            var list = new List<FigureInfo>(_list);
            _list.Clear();

            foreach (var figureInfo in list)
            {
                figureInfo.DestroySelf();
            }
        }

        private void ActivateAndClear()
        {
            foreach (var figureInfo in _list)
            {
                figureInfo.Activate();
            }
            _list.Clear();
        }

        public void ClearList()
        {
            _list.Clear();
        }
    }
}