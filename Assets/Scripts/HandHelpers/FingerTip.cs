using UnityEngine;

namespace HandHelpers
{
    public class FingerTip : MonoBehaviour
    {
        private void Start()
        {
            gameObject.layer = LayerMask.NameToLayer("FingerTip");
        }
    }
}