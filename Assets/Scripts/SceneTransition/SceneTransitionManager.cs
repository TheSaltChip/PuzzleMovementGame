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
        [SerializeField] private FaderScreen faderScreen;
        [SerializeField] private BoolVariable sceneChanged;
 
        public UnityEvent onSceneEnter;
        public UnityEvent onSceneExit;

        private AsyncOperation _loadLevelOperation;

        private void Start()
        {
            if (!sceneChanged.value) return;

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

        private void HandleSceneChange()
        {
            StartCoroutine(HandleSceneChangeCoroutine());
        }

        private IEnumerator HandleSceneChangeCoroutine()
        {
            onSceneEnter?.Invoke();

            yield return StartCoroutine(faderScreen.FadeRoutine(Color.black, Color.clear));

            _loadLevelOperation = null;
        }
    }
}