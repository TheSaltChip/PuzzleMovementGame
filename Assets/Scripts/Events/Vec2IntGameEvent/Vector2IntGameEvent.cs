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

namespace Events.Vec2IntGameEvent
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Events/Vector2IntGameEvent")]
    public class Vector2IntGameEvent : ScriptableObject
    {
        private List<Vector2IntGameEventListener> _listeners = new();

        public void Raise(Vector2Int value)
        {
            for (var i = _listeners.Count-1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised(value);
            }
        }

        public void RegisterListener(Vector2IntGameEventListener listener)
        {
            if (!_listeners.Contains(listener)) _listeners.Add(listener);
        }

        public void UnregisterListener(Vector2IntGameEventListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}