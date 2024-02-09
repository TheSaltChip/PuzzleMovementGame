using System;
using NaughtyAttributes;
using SceneTransition;
using UnityEngine;
using UnityEngine.Events;
using Variables;

namespace Lobby
{
    public class Gate : MonoBehaviour
    {
        [SerializeField] private bool staticGate;

        [SerializeField, HideIf("staticGate")] private SceneInfo sceneInfo;
        [SerializeField, ShowIf("staticGate")] private StringVariable targetSceneName;

        private bool _active;
        private bool _collided;

        public UnityEvent<string> sceneNameToTeleportTo;

        private void OnCollisionEnter()
        {
            if (_collided || (!_active && !staticGate)) return;
            
            _collided = true;
            sceneNameToTeleportTo?.Invoke(staticGate ? targetSceneName.value : sceneInfo.sceneName);
        }

        public void ActivateGate()
        {
            _active = true;
        }
    }
}