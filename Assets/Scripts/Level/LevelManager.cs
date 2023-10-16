using Level.Completables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeReference] private Completable task;
        private GameObject[] _pointsOfInterest;

        void Start()
        {
            _pointsOfInterest = GameObject.FindGameObjectsWithTag("Interest");
            CompassSystem.Instance.SetTarget(_pointsOfInterest[0].transform);
        }

        private void Update()
        {
            if (task.IsDone)
            {
                print("Done");
            }
        }
    }
}