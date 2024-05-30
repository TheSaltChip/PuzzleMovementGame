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

using ThrowingOnTargets.Saveable;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using Util.PRS;

namespace ThrowingOnTargets
{
    public class ThrowAtTargetGridCreator : MonoBehaviour
    {
        [SerializeField] private TargetInfo targetInfo;
        [SerializeField, Min(0)] private float padding;
        
        private float LengthBetweenClosestTargets => 0.5f + padding;

        public void CreateGrid(int n)
        {
            var ringSum = 1 + 6 * (n * (n + 1) / 2);

            var ringNum = 0;
            var targetNum = 0;

            var places = new Vector3[ringSum];
            var point = Vector3.zero;

            var step = 1;

            for (var i = 1; i < ringSum; ++i, ++targetNum)
            {
                if (i == 1 || CheckI(i, n))
                {
                    point = new Vector3(LengthBetweenClosestTargets * ringNum, 0, 0);
                    ++ringNum;
                    targetNum = 0;
                    step = 0;
                }

                if (step == 1)
                {
                    step = 2;
                }

                point += new Vector3(
                    LengthBetweenClosestTargets * Mathf.Cos(step * Mathf.PI / 3f),
                    LengthBetweenClosestTargets * Mathf.Sin(step * Mathf.PI / 3f),
                    0);

                if (targetNum % ringNum == 0)
                {
                    ++step;
                }

                places[i] = point;
            }

            targetInfo.grid = places;
            
            var stage = new Stage { posRots = new PosRotScl[places.Length] };

            for (var i = 0; i < places.Length; i++)
            {
                stage.posRots[i] = new PosRotScl()
                {
                    position = places[i],
                    rotation = new Vector3(90, 0, 180),
                    scale = Vector3.one
                };
            }
        }
        
        private bool CheckI(int i, int n)
        {
            for (var j = 1; j <= n; j++)
            {
                if (i == 1 + 6 * (j * (j + 1) / 2)) return true;
            }

            return false;
        }
    }
}