using System.Collections;
using UnityEngine;

namespace SceneTransition.TransitionScriptableObject
{
    [CreateAssetMenu(menuName = "Scene Transitions/Fade", fileName = "Fade")]
    public class FadeTransitionScriptableObject : AbstractSceneTransitionScriptableObject
    {
        public override IEnumerator Exit(Canvas parent)
        {
            AnimatedObject = CreateImage(parent);
            AnimatedObject.rectTransform.anchorMin = Vector2.zero;
            AnimatedObject.rectTransform.anchorMax = Vector2.one;
            AnimatedObject.rectTransform.sizeDelta = Vector2.zero;

            var time = 0f;
            var startColor = new Color(0, 0, 0, 0);
            var endColor = Color.black;
            
            while (time < 1)
            {
                AnimatedObject.color = Color.Lerp(
                    startColor, 
                    endColor, 
                    lerpCurve.Evaluate(time)
                );
                yield return null;
                time += Time.deltaTime / animationTime;
            }
        }

        public override IEnumerator Enter(Canvas parent)
        {
            float time = 0;
            Color startColor = Color.black;
            Color endColor = new Color(0, 0, 0, 0);
            while (time < 1)
            {
                AnimatedObject.color = Color.Lerp(
                    startColor, 
                    endColor, 
                    lerpCurve.Evaluate(time)
                );
                yield return null;
                time += Time.deltaTime / animationTime;
            }

            Destroy(AnimatedObject.gameObject);
        }
    }
}