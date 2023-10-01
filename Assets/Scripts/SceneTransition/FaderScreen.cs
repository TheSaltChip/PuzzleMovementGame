using System.Collections;
using UnityEngine;

namespace SceneTransition
{
    [RequireComponent(typeof(Renderer))]
    public class FaderScreen : MonoBehaviour
    {
        public float animationTime = 0.5f;

        private Renderer _renderer;

        private static readonly int Color1 = Shader.PropertyToID("_Color");

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            SceneTransitioner.Instance.OnSceneExitCoroutine += FadeIn;
            SceneTransitioner.Instance.OnSceneEnterCoroutine += FadeOut;
        }

        private void OnDisable()
        {
            SceneTransitioner.Instance.OnSceneExitCoroutine -= FadeIn;
            SceneTransitioner.Instance.OnSceneEnterCoroutine -= FadeOut;
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