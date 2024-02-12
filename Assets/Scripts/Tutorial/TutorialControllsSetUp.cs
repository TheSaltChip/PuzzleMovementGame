using UnityEngine;

namespace Lobby
{
    public class TutorialControllsSetUp : MonoBehaviour
    {
        [SerializeField] private GameObject controllerL;
        [SerializeField] private GameObject controllerR;
        [SerializeField] private SkinnedMeshRenderer handLInner;
        [SerializeField] private SkinnedMeshRenderer handLOuter;
        [SerializeField] private SkinnedMeshRenderer handRInner;
        [SerializeField] private SkinnedMeshRenderer handROuter;

        private bool tutorial = true;

        public void StartTutorial()
        {
            controllerL.SetActive(tutorial);
            controllerR.SetActive(tutorial);
            handLInner.enabled = !tutorial;
            handLOuter.enabled = !tutorial;
            handRInner.enabled = !tutorial;
            handROuter.enabled = !tutorial;
            tutorial = false;
        }

        public void EndTutorial()
        {
            controllerL.SetActive(tutorial);
            controllerR.SetActive(tutorial);
            handLInner.enabled = !tutorial;
            handLOuter.enabled = !tutorial;
            handRInner.enabled = !tutorial;
            handROuter.enabled = !tutorial;
        }
    }
}
