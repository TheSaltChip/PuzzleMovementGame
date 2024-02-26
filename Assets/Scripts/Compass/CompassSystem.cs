using UnityEngine;

namespace Compass
{
    public class CompassSystem : MonoBehaviour
    {
        public static CompassSystem Instance { get; private set; }

        [SerializeField] private GuideToPoint needle;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning(
                    $"Invalid configuration. Duplicate Instances found! First one: {Instance.name} Second one: {name}. Destroying second one.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void SetTarget(Transform target)
        {
            needle.SetTarget(target);
        }
    }
}
