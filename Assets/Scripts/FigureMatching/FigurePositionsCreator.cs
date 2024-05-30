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

using FigureMatching.ScriptableObjects;
using UnityEngine;

namespace FigureMatching
{
    public class FigurePositionsCreator : MonoBehaviour
    {
        [SerializeField] private FigurePositions positions;
        [SerializeField] private FigureSpawnerSettings settings;

        public void Create()
        {
            positions.Clear();
            var deltaTheta = settings.deltaThetaDeg * Mathf.Deg2Rad;
            var deltaPhi = settings.deltaPhiDeg * Mathf.Deg2Rad;
            var radius = settings.radius;

            const float tau = 2f * Mathf.PI;
            const float piDiv2 = Mathf.PI / 2f;
            
            for (var theta = piDiv2; theta > Mathf.PI / 6 + 1e-6f; theta -= deltaTheta)
            {
                for (var phi = 0f; phi < tau - 1e-6f; phi += deltaPhi)
                {
                    var pos = new Vector3(
                        radius * Mathf.Cos(phi) * Mathf.Sin(theta),
                        radius * Mathf.Cos(theta),
                        radius * Mathf.Sin(phi) * Mathf.Sin(theta));
                    positions.Add(pos);
                }
            }
        }
    }
}