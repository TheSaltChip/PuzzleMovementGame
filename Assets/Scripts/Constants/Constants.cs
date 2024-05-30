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

using System.IO;
using UnityEngine.Device;

namespace Constants
{
    public static class PlayerPrefsNames
    {
        public const string Turn = "turn";
        public const string TurnSpeed = "turnSpeed";
        public const string SnapTurnAngle = "snapTurnAngle";
    }

    public static class PathNames
    {
        public static readonly string Levels = Path.Combine(Application.dataPath, "Levels");
        public static readonly string Throwable = Path.Combine(Levels, "Throwable");
        public static readonly string CustomThrowable = Path.Combine(Throwable, "Custom");
    }
}