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

namespace Util
{
    public class GizmoWireframeDiamond : MonoBehaviour
    {
        [SerializeField] private float radius = 0.5f;

        private void OnDrawGizmos()
        {
            var half = radius * 0.5f;
            var transform1 = transform;
            var rotation = transform1.rotation;

            var x = rotation * (Vector3.right) * half;
            var y = rotation * (Vector3.up) * half;
            var z = rotation * (Vector3.forward) * half;
            var position = transform1.localPosition;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(position, position + x);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(position - x, position);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(position, position + y);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(position - y, position);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(position, position + z);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(position - z, position);

            Gizmos.DrawLine(position + x, position + z);
            Gizmos.DrawLine(position + x, position - z);
            Gizmos.DrawLine(position - x, position + z);
            Gizmos.DrawLine(position - x, position - z);

            Gizmos.DrawLine(position + y, position + z);
            Gizmos.DrawLine(position + y, position - z);
            Gizmos.DrawLine(position + y, position - x);
            Gizmos.DrawLine(position + y, position + x);

            Gizmos.DrawLine(position - y, position + z);
            Gizmos.DrawLine(position - y, position - z);
            Gizmos.DrawLine(position - y, position - x);
            Gizmos.DrawLine(position - y, position + x);
        }
    }
}