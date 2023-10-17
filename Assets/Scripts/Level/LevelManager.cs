
using Level.Completables;
using NaughtyAttributes;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private bool noTask;
        [SerializeReference, HideIf("noTask")] private Completable task;
        [SerializeField] private Transform startingPosition;

        public static LevelManager Instance { get; private set; }
        
        public Vector3 GetStartingPosition()
        {
            return startingPosition.position;
        }
        
        public Quaternion GetStartingRotation()
        {
            return startingPosition.rotation;
        }

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
            if (startingPosition != null) return;
            
            var go = GameObject.FindWithTag("SpawnPoint");

            startingPosition = go == null ? transform : go.transform;
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