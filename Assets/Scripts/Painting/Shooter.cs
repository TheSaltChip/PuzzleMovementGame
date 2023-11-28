using System;
using CardMemorization;
using Unity.VisualScripting;
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

        public void Shoot()
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