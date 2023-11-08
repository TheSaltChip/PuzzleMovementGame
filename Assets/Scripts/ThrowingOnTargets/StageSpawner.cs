using Events;
using ThrowingOnTargets.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Util;

namespace ThrowingOnTargets
{
    public class StageSpawner : MonoBehaviour
    {
        [SerializeField] private Stages stages;
        [SerializeField] private GameObject target;

        private IObjectPool<GameObject> _targets;

        private void Awake()
        {
            _targets = new ObjectPool<GameObject>(CreateTarget, GetFromPool, OnReleaseToPool, DestroyFromPool);
        }

        private void Start()
        {
            if (stages.stages.Length == 0) return;
            SetupStage();
        }

        public void SetupStage()
        {
            var stageLocations = stages.CurrentStage();
            var locations = stageLocations.locations;
            var rotations = stageLocations.locations;
            
            for (var i = 0; i < locations.Length; i++)
            {
                var t = _targets.Get();

                t.transform.SetLocalPositionAndRotation(locations[i], Quaternion.Euler(rotations[i]));
            }
        }

        private GameObject CreateTarget()
        {
            var go = Instantiate(target, transform, true);

            if (go.TryGetComponent<ReturnToPool>(out var rtp))
                rtp.pool = _targets;

            return go;
        }

        private void GetFromPool(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void OnReleaseToPool(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void DestroyFromPool(GameObject obj)
        {
            Destroy(obj);
        }
    }
}