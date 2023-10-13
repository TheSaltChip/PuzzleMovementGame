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
        private GuideToPoint _compassScript;

        void Start()
        {
            _compassScript = GameObject.FindGameObjectWithTag("Compass").GetComponent<GuideToPoint>();
            _pointsOfInterest = GameObject.FindGameObjectsWithTag("Interest");
            _compassScript.SetTarget(_pointsOfInterest[0].transform);
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