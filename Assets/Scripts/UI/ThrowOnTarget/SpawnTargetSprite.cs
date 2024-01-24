using UnityEngine;

namespace UI.ThrowOnTarget
{
    [ExecuteInEditMode]
    public class SpawnTargetSprite : MonoBehaviour
    {
        [SerializeField] private GameObject targetSprite;
        [SerializeField] private bool spawn;
        

        public void SpawnTarget()
        {
            Instantiate(targetSprite, transform);
        }
        
        private void Update()
        {
            if (!spawn) return;
            
            spawn = false;
            
            SpawnTarget();
        }
    }
}