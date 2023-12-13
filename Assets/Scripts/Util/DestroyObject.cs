using UnityEngine;

namespace Util
{
    public class DestroyObject : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}