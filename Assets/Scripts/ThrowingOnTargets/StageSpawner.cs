using System.Collections;
using System.Collections.Generic;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using Util;
using Variables;

namespace ThrowingOnTargets
{
    public class StageSpawner : MonoBehaviour
    {
        [SerializeField] private ThrowLevelSO throwLevel;
        [SerializeField] private GameObject target;
        [SerializeField] private IntVariable targetsInStage;
        [SerializeField] private TargetInfo targetInfo;

        public UnityEvent onStageReady;
        
        private IObjectPool<GameObject> _targets;
        private Coroutine _setupCoroutine;
        private WaitForSeconds _waitFor100Milliseconds;

        private void Awake()
        {
            var t = transform;
            targetInfo.spawnPoint = t.localPosition;
            _targets = new ObjectPool<GameObject>(() =>
            {
                var go = Instantiate(target, transform, true);

                if (go.TryGetComponent<ReturnToPool>(out var rtp))
                    rtp.pool = _targets;

                return go;
            }, o => o.SetActive(true), obj => obj.SetActive(false), Destroy);
            _waitFor100Milliseconds = new WaitForSeconds(0.1f);
        }

        public void SetupStage()
        {
            var stageLocations = throwLevel.CurrentStage();

            var posRots = stageLocations.posRots;

            if (posRots == null)
            {
                Debug.LogError("No positions for targets found");
                return;
            };
            
            targetsInStage.value = posRots.Length;

            StartCoroutine(SetStageCoroutine(posRots));
            
            onStageReady?.Invoke();
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
    }
}