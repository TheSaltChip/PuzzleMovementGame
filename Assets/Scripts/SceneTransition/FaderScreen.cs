using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Variables;

namespace SceneTransition
{
    [CreateAssetMenu(menuName = "XRRig/FaderScreen", fileName = "FaderScreen")]
    public class FaderScreen : ScriptableObject
    {
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        [SerializeField] private FloatVariable animationTime;
        [SerializeField] private Material material;

        private void Awake()
        {
            material.SetColor(Color1, Color.clear);
        }

        public IEnumerator FadeRoutine(Color from, Color to)
        {
            var time = 0f;

            while (time <= animationTime.value)
            {
                material.SetColor(Color1, Color.Lerp(
                    from,
                    to,
                    time / animationTime.value
                ));

                time += Time.deltaTime;

                yield return null;
            }

            material.SetColor(Color1, to);
        }
    }
}