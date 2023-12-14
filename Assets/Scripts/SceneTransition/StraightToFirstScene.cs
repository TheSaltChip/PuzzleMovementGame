using UnityEngine;
using Variables;

namespace SceneTransition
{
    public class StraightToFirstScene : MonoBehaviour
    {
        [SerializeField] private StringVariable sceneName;
        
        private void Start()
        {
            SceneTransitionManager.Instance.LoadScene(sceneName.value);
        }
    }
}