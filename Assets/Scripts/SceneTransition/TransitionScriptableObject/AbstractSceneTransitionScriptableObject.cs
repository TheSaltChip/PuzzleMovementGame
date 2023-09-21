using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SceneTransition.TransitionScriptableObject
{
    public abstract class AbstractSceneTransitionScriptableObject : ScriptableObject
    {
        public AnimationCurve lerpCurve;
        public float animationTime = 0.25f;
        protected Image AnimatedObject;
        
        public abstract IEnumerator Exit(Canvas parent);

        public abstract IEnumerator Enter(Canvas parent);

        protected virtual Image CreateImage(Canvas parent)
        {
            var child = new GameObject("Transition Image");
            child.transform.SetParent(parent.transform, false);

            return child.AddComponent<Image>();
        }
    }
}