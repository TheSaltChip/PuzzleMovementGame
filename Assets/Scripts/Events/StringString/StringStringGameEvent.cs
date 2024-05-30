﻿#region License
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
using UnityEngine;

namespace Events.StringString
{
    [CreateAssetMenu(fileName = "StringStringGameEvent", menuName = "Events/StringStringGameEvent")]
    public class StringStringGameEvent : ScriptableObject
    {
        private List<StringStringGameEventListener> _listeners = new();

        public void Raise(string str1, string str2)
        {
            for (var i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(str1,str2);
            }
        }

        public void RegisterListener(StringStringGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(StringStringGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}