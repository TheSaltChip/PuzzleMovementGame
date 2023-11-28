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

            if (!gameObject.activeSelf) return;
            pool?.Release(gameObject);
        }

        public void ReturnByName(Object obj)
        {
            if (!gameObject.activeSelf) return;
            if (obj.name == gameObject.name)
            {
                pool?.Release(gameObject);
                print(gameObject.name);
            }
        }
    }
}