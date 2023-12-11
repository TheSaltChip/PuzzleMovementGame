using UnityEngine;

namespace Tutorial
{
    public class TutorialButtonHandler : MonoBehaviour
    {
        [SerializeField] private GameObject button;

        public void First()
        {
            button.SetActive(!button.name.Equals("Previous"));
        }

        public void Last()
        {
            button.SetActive(!button.name.Equals("Next"));
        }

        public void Middle()
        {
            if(!button.activeSelf)
                button.SetActive(true);
        }
    }
}
