using System.Collections;
using UnityEngine;

namespace SceneTransition
{
    public class FaderScreen : MonoBehaviour
    {
        private static readonly int Color1 = Shader.PropertyToID("_Color");
        
        [SerializeField] private float animationTime = 0.5f;
        [SerializeField] private Renderer _renderer;
        
        public void FadeIn()
        {
            StartCoroutine(FadeRoutine(new Color(0, 0, 0, 0), Color.black));
        }

        public void FadeOut()
        {
            StartCoroutine(FadeRoutine(Color.black, new Color(0, 0, 0, 0)));
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