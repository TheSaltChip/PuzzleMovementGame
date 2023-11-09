using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using UnityEngine.Pool;
using Util;
using Variables;

namespace ThrowingOnTargets
{
    public class StageSpawner : MonoBehaviour
    {
        [SerializeField] private StagesSO stages;
        [SerializeField] private GameObject target;
        [SerializeField] private IntVariable targetsInStage;

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

            var posRots = stageLocations.posRots;

            targetsInStage.value = posRots.Length;

            for (var i = 0; i < posRots.Length; i++)
            {
                var t = _targets.Get();

                t.transform.SetLocalPositionAndRotation(
                    posRots[i].location,
                    Quaternion.Euler(posRots[i].rotation));
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