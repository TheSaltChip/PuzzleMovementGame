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
using UnityEngine;

namespace Events.FigureInfo
{
    [CreateAssetMenu(fileName = "FigureInfoGameEvent", menuName = "Events/FigureInfoGameEvent")]
    public class FigureInfoGameEvent : ScriptableObject
    {
        private List<FigureInfoGameEventListener> _listeners = new();

        public void Raise(FigureMatching.FigureInfo info)
        {
            for (var i = _listeners.Count-1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(info);
            }
        }

        public void RegisterListener(FigureInfoGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(FigureInfoGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}