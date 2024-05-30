#region License
// Copyright (C) 2024 Sebastian Misje Jonassen & Mathias Nupen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Commons Clause License version 1.0 with GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Commons Clause License and GNU General Public License for more details.
// 
// You should have received a copy of the Commons Clause License and GNU General Public License
// along with this program.  If not, see <https://commonsclause.com/> and <https://www.gnu.org/licenses/>.
#endregion

using System.Collections;
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
        [SerializeField] private BoolVariable fadeOutCompleted;

        public UnityEvent onSceneEnter;
        public UnityEvent onSceneExit;

        private AsyncOperation _loadLevelOperation;
        private  Coroutine _loadSceneCoroutine;

        private void Start()
        {
            if (!sceneChanged.value) return;

            sceneChanged.value = false;
            HandleSceneChange();
        }

        public void LoadScene(string sceneName)
        {
            _loadSceneCoroutine ??= StartCoroutine(LoadSceneCoroutine(sceneName));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName)
        {
            _loadLevelOperation = SceneManager.LoadSceneAsync(sceneName);
            _loadLevelOperation.allowSceneActivation = false;

            yield return StartCoroutine(faderScreen.FadeRoutine(Color.clear, Color.black));

            onSceneExit?.Invoke();

            _loadLevelOperation.allowSceneActivation = true;
            sceneChanged.value = true;
            fadeOutCompleted.value = true;
        }

        private void HandleSceneChange()
        {
            StartCoroutine(HandleSceneChangeCoroutine());
        }

        private IEnumerator HandleSceneChangeCoroutine()
        {
            fadeOutCompleted.value = false;
            
            onSceneEnter?.Invoke();

            yield return StartCoroutine(faderScreen.FadeRoutine(Color.black, Color.clear));
        }
    }
}