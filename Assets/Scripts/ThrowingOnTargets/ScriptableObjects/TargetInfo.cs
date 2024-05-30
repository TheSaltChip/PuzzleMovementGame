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

namespace ThrowingOnTargets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TargetInfo", menuName = "ThrowAtTargets/TargetInfo", order = 0)]
    public class TargetInfo : ScriptableObject
    {
        public Vector3 spawnPoint;
        
        public Vector3[] grid;

        public Vector3[] GridRing1 => GetRing(1);
        public Vector3[] GridRing1To2 => GetRing(2);

        private Vector3[] GetRing(int n)
        {
            var newArr = new Vector3[1 + 6 * (n * (n + 1) / 2)];

            for (var i = 0; i < newArr.Length; i++)
            {
                newArr[i] = grid[i];
            }

            return newArr;
        }
    }
}