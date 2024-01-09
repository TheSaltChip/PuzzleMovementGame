using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Variables;

namespace SceneTransition
{
    public class FaderScreen : MonoBehaviour
    {
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        [SerializeField] private FloatVariable animationTime;
        [SerializeField] private Image image;

        private void Awake()
        {
            image.material.SetColor(Color1, Color.clear);
        }

        public void FadeIn()
        {
            StartCoroutine(FadeRoutine(Color.clear, Color.black, animationTime.value));
        }

        public void FadeOut()
        {
            StartCoroutine(FadeRoutine(Color.black, Color.clear, animationTime.value * 1.5f));
        }

        private IEnumerator FadeRoutine(Color from, Color to, float animTime)
        {
            var time = 0f;

            while (time <= animTime)
            {
                image.material.SetColor(Color1, Color.Lerp(
                    from,
                    to,
                    time / animTime
                ));

                time += Time.deltaTime;

                yield return null;
            }

            image.material.SetColor(Color1, to);
        }
    }
}