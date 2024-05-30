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
using UnityEngine.Pool;
using Util;

namespace Painting
{
    public class Shooter : MonoBehaviour
    {
        private Color _activeColor = Color.black;
        [SerializeField] private GameObject paintballPrefab;
        [SerializeField] private float projectileSpeed;
        
        private Quaternion _rot;
        private ObjectPool<GameObject> _paintballPool;

        public void SetColor(Color color)
        {
            _activeColor = color;
        }

        private void Awake()
        {
            _rot = this.transform.rotation;
            _paintballPool = new ObjectPool<GameObject>(CreateToPool, GetFromPool, OnReleaseToPool, DestroyFromPool);
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = _paintballPool.Get();
                _paintballPool.Release(obj);
            }
        }
        
        private GameObject CreateToPool()
        {
            GameObject temp = Instantiate(paintballPrefab, transform, true);

            var ret = temp.GetComponent<ReturnToPool>();
            
            ret.pool = _paintballPool;

            return temp;
        }

        private void GetFromPool(GameObject obj)
        {
            obj.SetActive(true);
            obj.GetComponent<CollisionPainter>().SetColor(_activeColor);
        }

        private void OnReleaseToPool(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void DestroyFromPool(GameObject obj)
        {
            Destroy(obj);
        }

        void Shoot()
        {
            var ball = _paintballPool.Get();
            var rb = ball.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            var tr = transform;
            var speed =  tr.forward*projectileSpeed;
            ball.transform.position = tr.position;
            rb.AddForce(speed);
        }
        
    }
}