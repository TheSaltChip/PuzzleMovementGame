using System.Collections;
using UnityEngine;

namespace SceneTransition
{
    [RequireComponent(typeof(Renderer))]
    public class FaderScreen : MonoBehaviour
    {
        private static readonly int Color1 = Shader.PropertyToID("_Color");
        public float animationTime = 0.5f;

        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            StartCoroutine(Init());
        }

        private void OnDisable()
        {
            SceneTransitionManager.Instance.OnSceneExitCoroutine -= FadeIn;
            SceneTransitionManager.Instance.OnSceneEnterCoroutine -= FadeOut;
        }

        private IEnumerator Init()
        {
            yield return new WaitUntil(() => SceneTransitionManager.Instance != null);

            SceneTransitionManager.Instance.OnSceneExitCoroutine += FadeIn;
            SceneTransitionManager.Instance.OnSceneEnterCoroutine += FadeOut;
        }

        private IEnumerator FadeIn()
        {
            yield return StartCoroutine(FadeRoutine(new Color(0, 0, 0, 0), Color.black));
        }

        private IEnumerator FadeOut()
        {
            yield return StartCoroutine(FadeRoutine(Color.black, new Color(0, 0, 0, 0)));
        }

        private IEnumerator FadeRoutine(Color from, Color to)
        {
            var time = 0f;

            while (time <= animationTime)
            {
                _renderer.material.SetColor(Color1, Color.Lerp(
                    from,
                    to,
                    time / animationTime
                ));

                time += Time.deltaTime;

                yield return null;
            }

            _renderer.material.SetColor(Color1, to);
        }
    }
}