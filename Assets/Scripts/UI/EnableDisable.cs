using UnityEngine;

namespace UI
{
    public class EnableDisable : MonoBehaviour
    {
        public void ChangeState()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}