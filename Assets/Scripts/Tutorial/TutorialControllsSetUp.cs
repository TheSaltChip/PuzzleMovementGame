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

        public void StartTutorial()
        {
            controllerL.SetActive(true);
            controllerR.SetActive(true);
            handLInner.enabled = false;
            handLOuter.enabled = false;
            handRInner.enabled = false;
            handROuter.enabled = false;
        }

        public void EndTutorial()
        {
            controllerL.SetActive(false);
            controllerR.SetActive(false);
            handLInner.enabled = true;
            handLOuter.enabled = true;
            handRInner.enabled = true;
            handROuter.enabled = true;
        }
    }
}
