using System.Collections;
using System.Collections.Generic;
using Events;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Util;
using Variables;

namespace ThrowingOnTargets
{
    public class StageSpawner : MonoBehaviour
    {
        [SerializeField] private ThrowLevelSO throwLevel;
        [SerializeField] private GameObject target;
        [SerializeField] private IntVariable targetsInStage; 
            
        private IObjectPool<GameObject> _targets;
        private Coroutine _setupCoroutine;
        private WaitForSeconds _waitFor100Milliseconds;

        private void Awake()
        {
            _targets = new ObjectPool<GameObject>(CreateTarget, GetFromPool, OnReleaseToPool, DestroyFromPool);
            _waitFor100Milliseconds = new WaitForSeconds(0.1f);
        }

        public void SetupStage()
        {
            var stageLocations = throwLevel.CurrentStage();

            var posRots = stageLocations.posRots;

            targetsInStage.value = posRots.Length;
            
            StartCoroutine(SetStageCoroutine(posRots));
        }

        private IEnumerator SetStageCoroutine(IList<PosRotScl> posRotScl)
        {
            for (var i = 0; i < posRotScl.Count; i++)
            {
                yield return _waitFor100Milliseconds;
                var t = _targets.Get();

                t.transform.SetLocalPositionAndRotation(
                    posRotScl[i].location,
                    Quaternion.Euler(posRotScl[i].rotation));
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