using Level.Completables;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeReference] private Completable task;


        private void Update()
        {
            if (task.IsDone())
            {
            }
        }
    }
}