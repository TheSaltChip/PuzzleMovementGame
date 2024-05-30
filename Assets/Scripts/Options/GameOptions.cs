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
using Variables;

namespace Options
{
    [CreateAssetMenu(fileName = "GameOptions", menuName = "Options/GameOptions")]
    public class GameOptions : ScriptableObject
    {
        public IntVariable rotationType;
        public FloatVariable snapTurnAngle;
        public FloatVariable smoothTurnSpeed;

        public int RotationType
        {
            get => rotationType.value;
            set => rotationType.value = value;
        }

        public float SnapTurnAngle
        {
            get => snapTurnAngle.value;
            set => snapTurnAngle.value = value;
        }

        public float SmoothTurnSpeed
        {
            get => smoothTurnSpeed.value;
            set => smoothTurnSpeed.value = value;
        }
    }
}