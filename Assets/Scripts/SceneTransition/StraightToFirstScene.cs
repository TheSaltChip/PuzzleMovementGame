using UnityEngine;

namespace SceneTransition
{
    public class StraightToFirstScene : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        
        private void Start()
        {
            SceneTransitionManager.Instance.LoadScene(sceneName);
        }
    }
}