using Events.String;
using UnityEngine;
using Variables;

namespace SceneTransition
{
    public class StraightToFirstScene : MonoBehaviour
    {
        [SerializeField] private StringVariable sceneName;
        [SerializeField] private StringGameEvent loadScene;
        
        private void Start()
        {
            loadScene.Raise(sceneName.value);
        }
    }
}