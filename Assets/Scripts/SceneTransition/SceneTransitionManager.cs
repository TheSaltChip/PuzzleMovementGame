﻿using Autohand;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

        private AsyncOperation _loadLevelOperation;

        public static SceneTransitionManager Instance { get; private set; }

        public UnityEvent onSceneChanged;
        public UnityEvent onSceneExit;
        public UnityEvent onSceneEnter;

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
        }

        public void LoadScene(string sceneName)
        {
            _loadLevelOperation = SceneManager.LoadSceneAsync(sceneName);
            _loadLevelOperation.allowSceneActivation = false;

            onSceneExit?.Invoke();

            _loadLevelOperation.allowSceneActivation = true;
        }


        private void SetStartPositionAndRotation()
        {
            var go = GameObject.FindWithTag("SpawnPoint");

            if (go != null)
            {
                _startingPosRot = new PosRot()
                {
                    Position = go.transform.position,
                    Rotation = go.transform.rotation
                };
            }

            AutoHandPlayer.Instance.SetPosition(
                _startingPosRot.Position,
                _startingPosRot.Rotation);
        }

        private void HandleSceneChange(Scene oldScene, Scene newScene)
        {
            onSceneChanged?.Invoke();

            SetStartPositionAndRotation();

            onSceneEnter?.Invoke();

            _loadLevelOperation = null;
        }
    }
}