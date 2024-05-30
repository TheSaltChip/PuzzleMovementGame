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

using System.Collections;
using UnityEngine;

namespace Util
{
    public class Throwable : MonoBehaviour
    {
        private Vector3 _buildUp;
        private Vector3 _position;
        private Rigidbody _rigidBody;
        private bool _useForce;
        private int _layer;

        private void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _layer = LayerMask.NameToLayer("Throwable");
            gameObject.layer = _layer;
        }

        public void Released()
        {
            _useForce = true;
            gameObject.layer = LayerMask.NameToLayer("Hand");
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            for (var i = 0; i < 6; i++)
            {
                yield return null;
            }
        
            gameObject.layer = _layer;
        }

        private void FixedUpdate()
        {
            if (!_useForce) return;
            _useForce = false;
            _rigidBody.AddForce(_rigidBody.velocity * (_rigidBody.mass * 1.1f), ForceMode.Impulse);
        
        }
    }
}
