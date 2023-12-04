using UnityEngine;
using UnityEngine.Pool;

namespace Util
{
    public class ReturnToPool : MonoBehaviour
    {
        public IObjectPool<GameObject> pool;

        public void Return()
        {
            // Return to the pool
            print($"Name: {name} {gameObject.activeSelf}");
            print(pool);
            if (!gameObject.activeSelf) return;

            pool?.Release(gameObject);
        }
    }
}