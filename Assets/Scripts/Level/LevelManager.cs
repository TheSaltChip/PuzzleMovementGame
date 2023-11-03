using Compass;
using Completables;
using NaughtyAttributes;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private bool noTask;
        [SerializeReference, HideIf("noTask")] private Completable task;

        public static LevelManager Instance { get; private set; }

        private GameObject[] _pointsOfInterest;
        
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


        private void Start()
        {
            _pointsOfInterest = GameObject.FindGameObjectsWithTag("Interest");
            CompassSystem.Instance.SetTarget(_pointsOfInterest[0].transform);
        }

        private void Update()
        {
            if (!noTask && task.IsDone)
            {
                print("Done");
            }
        }
    }
}