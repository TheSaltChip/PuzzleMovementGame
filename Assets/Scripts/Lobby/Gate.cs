using SceneTransition;
using UnityEngine;

namespace Lobby
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private SceneInfo sceneInfo;

        private bool _collided;
        private bool _active;

        private void OnCollisionEnter()
        {
            if (_collided || !_active) return;
            _collided = true;
            SceneTransitionManager.Instance.LoadScene(sceneInfo.sceneName);
        }

        public void ActivateGate()
        {
            _active = true;
        }

        public void DeactivateGate()
        {
            _active = false;
        }
    }
}