using SceneTransition;
using UnityEngine;

namespace Lobby
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private SceneInfo sceneInfo;
        [SerializeField] private bool active;

        private bool _collided;

        private void OnCollisionEnter()
        {
            if (_collided || !active) return;
            _collided = true;
            SceneTransitionManager.Instance.LoadScene(sceneInfo.sceneName);
        }

        public void ActivateGate()
        {
            active = true;
        }

        public void DeactivateGate()
        {
            active = false;
        }
    }
}