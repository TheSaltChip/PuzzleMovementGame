using System;
using System.Collections;
using System.Collections.Generic;
using SceneTransition.TransitionScriptableObject;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneTransition
{
    [DisallowMultipleComponent, RequireComponent(typeof(Canvas))]
    public class SceneTransitioner : MonoBehaviour
    {
        public static SceneTransitioner Instance { get; private set; }

        [SerializeField] private List<Transition> transitions = new();

        private Canvas _transitionCanvas;

        private AsyncOperation _loadLevelOperation;
        private AbstractSceneTransitionScriptableObject _activeTransition;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning(
                    $"Invalid configuration. Duplicate Instances found! First one: {Instance.name} Second one: {name}. Destroying second one.");
                Destroy(gameObject);
                return;
            }

            SceneManager.activeSceneChanged += HandleSceneChange;

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _transitionCanvas = GetComponent<Canvas>();
            _transitionCanvas.enabled = false;
        }


        public void LoadScene(string sceneName,
            SceneTransitionMode transitionMode = SceneTransitionMode.None,
            LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            _loadLevelOperation = SceneManager.LoadSceneAsync(sceneName, sceneMode);

            var transition = transitions.Find(t => t.mode == transitionMode);

            if (transition == null)
            {
                Debug.LogWarning($"No transition found for TransitionMode {transitionMode}! " +
                                 $"Maybe you are missing a configuration?");
                return;
            }


            _loadLevelOperation.allowSceneActivation = false;
            _transitionCanvas.enabled = true;
            _activeTransition = transition.animationSO;
            StartCoroutine(Exit());
        }
        

        private IEnumerator Exit()
        {
            yield return StartCoroutine(_activeTransition.Exit(_transitionCanvas));
            _loadLevelOperation.allowSceneActivation = true;
        }
        
        private IEnumerator Enter()
        {
            yield return StartCoroutine(_activeTransition.Enter(_transitionCanvas));
            _transitionCanvas.enabled = false;
            _loadLevelOperation = null;
            _activeTransition = null;
        }

        private void HandleSceneChange(Scene oldScene, Scene newScene)
        {
            if (_activeTransition != null)
            {
                StartCoroutine(Enter());
            }
        }
    }

    [Serializable]
    public class Transition
    {
        public SceneTransitionMode mode;
        public AbstractSceneTransitionScriptableObject animationSO;
    }
}