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

namespace Compass
{
    public class GuideToPoint : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        private Transform _target;
        private bool _track = true;
        private bool _freeRoamed = false;
        private Transform _start;
        [SerializeField] private Transform transform1;

        private float _timeCount;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void TrackingTrue()
        {
            _start = transform;
            _freeRoamed = true;
            _track = true;
        }
    
        public void TrackingFalse()
        {
            _track = false;
        }

        private void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("CompassNeedle");
        }

        private void FixedUpdate()
        {
            if (!_track) return;
        
            if (_freeRoamed)
            {
            
                if (_timeCount >= 1)
                {
                    _freeRoamed = false;
                }
            
            
                transform1.LookAt(_target);
            
                _timeCount += Time.fixedDeltaTime;
            
                rb.MoveRotation(Quaternion.Slerp(_start.rotation, transform1.rotation, _timeCount));
            }
            else
            {
                Transform transform2;
                _timeCount = 0;
                (transform2 = transform).LookAt(_target);

                rb.MoveRotation(Quaternion.LookRotation(transform2.forward));
            }
        }
    }
}