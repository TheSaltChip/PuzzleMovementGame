using System;
using System.Collections;
using Autohand;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Variables;

namespace SceneTransition
{
    [DisallowMultipleComponent]
    public class SceneTransitionManager : MonoBehaviour
    {
        private struct PosRot
        {
            public Vector3 Position;
            public Quaternion Rotation;
        }

        private PosRot _startingPosRot = new()
        {
            Position = new Vector3(0, 0.5f, 0),
            Rotation = Quaternion.identity
        };

        [SerializeField] private FaderScreen faderScreen;
        [SerializeField] private BoolVariable sceneChanged;

        public UnityEvent onSceneChanged;
        public UnityEvent onSceneExit;
        public UnityEvent onSceneEnter;

        private AsyncOperation _loadLevelOperation;

        private void Start()
        {
            if(!sceneChanged.value) return;

            sceneChanged.value = false;
            HandleSceneChange();
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName));
        }

        public IEnumerator LoadSceneCoroutine(string sceneName)
        {
            _loadLevelOperation = SceneManager.LoadSceneAsync(sceneName);
            _loadLevelOperation.allowSceneActivation = false;

            yield return StartCoroutine(faderScreen.FadeRoutine(Color.clear, Color.black));

            onSceneExit?.Invoke();

            _loadLevelOperation.allowSceneActivation = true;
            sceneChanged.value = true;
        }
        // Custom method for calling fader screen and letting it fade completely out before changing scene


        private void SetStartPositionAndRotation()
        {
            var go = GameObject.FindWithTag("SpawnPoint");

            if (go != null)
            {
                _startingPosRot = new PosRot
                {
                    Position = go.transform.position,
                    Rotation = go.transform.rotation
                };
            }

            AutoHandPlayer.Instance.SetPosition(
                _startingPosRot.Position,
                _startingPosRot.Rotation);
        }

        private void HandleSceneChange()
        {
            StartCoroutine(HandleSceneChangeCoroutine());
        }

        private IEnumerator HandleSceneChangeCoroutine()
        {
            onSceneChanged?.Invoke();

            SetStartPositionAndRotation();
            
            onSceneEnter?.Invoke();
            
            yield return StartCoroutine(faderScreen.FadeRoutine(Color.black, Color.clear));

            _loadLevelOperation = null;
        }
    }
}