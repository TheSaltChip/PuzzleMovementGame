using System.Collections;
using Autohand;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SceneTransition
{
    [DisallowMultipleComponent]
    public class SceneTransitionManager : MonoBehaviour
    {
        public delegate IEnumerator SceneEventCoroutineHandler();

        private AsyncOperation _loadLevelOperation;
        public static SceneTransitionManager Instance { get; private set; }

        public UnityEvent OnSceneChanged;
        public UnityEvent OnSceneExit;
        public UnityEvent OnSceneEnter;

        public event SceneEventCoroutineHandler OnSceneExitCoroutine;
        public event SceneEventCoroutineHandler OnSceneEnterCoroutine;

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

            OnSceneChanged = new UnityEvent();
            OnSceneExit = new UnityEvent();
            OnSceneEnter = new UnityEvent();

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(string sceneName,
            LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            _loadLevelOperation = SceneManager.LoadSceneAsync(sceneName, sceneMode);
            _loadLevelOperation.allowSceneActivation = false;

            StartCoroutine(Exit());
        }


        private IEnumerator Exit()
        {
            OnSceneExit?.Invoke();
            if (OnSceneExitCoroutine != null)
                yield return StartCoroutine(OnSceneExitCoroutine());

            _loadLevelOperation.allowSceneActivation = true;
        }

        private IEnumerator Enter()
        {
            OnSceneEnter?.Invoke();
            if (OnSceneEnterCoroutine != null)
                yield return StartCoroutine(OnSceneEnterCoroutine());

            _loadLevelOperation = null;
        }

        private void HandleSceneChange(Scene oldScene, Scene newScene)
        {
            AutoHandPlayer.Instance.SetPosition(Vector3.zero);
            OnSceneChanged?.Invoke();

            StartCoroutine(Enter());
        }
    }
}