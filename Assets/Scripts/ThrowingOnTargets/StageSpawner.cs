using System.Collections;
using System.Collections.Generic;
using System.Text;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Util;
using Variables;

namespace ThrowingOnTargets
{
    public class StageSpawner : MonoBehaviour
    {
        [FormerlySerializedAs("throwLevel")] [SerializeField]
        private ThrowLevelRules throwLevelRules;

        [SerializeField] private GameObject target;
        [SerializeField] private IntVariable targetsInStage;
        [SerializeField] private TargetInfo targetInfo;

        public UnityEvent onStageReady;

        private IObjectPool<GameObject> _targets;
        private Coroutine _setupCoroutine;
        private WaitForSeconds _waitFor100Milliseconds;

        private struct Plane
        {
            public int[][] XYIndices;
        }

        private struct Depths
        {
            public Plane[] Planes;
        }

        private Depths _spawnablePositions;

        private float _length = 0.5f + 1f;

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

        public void OldSetupStage()
        {
            /*var stageLocations = throwLevel.CurrentStage();

            var posRots = stageLocations.posRots;

            if (posRots == null)
            {
                Debug.LogError("No positions for targets found");
                return;
            }

            targetsInStage.value = posRots.Length;

            StartCoroutine(SetStageCoroutine(posRots));

            onStageReady?.Invoke();*/
        }

        public void SetupStage()
        {
            _spawnablePositions = new Depths
            {
                Planes = new Plane[throwLevelRules.stages]
            };

            for (var i = 0; i < throwLevelRules.stages; i++)
            {
                _spawnablePositions.Planes[i].XYIndices = new int[throwLevelRules.xSize][];
                var index = 0;

                for (var j = 0; j < throwLevelRules.xSize; j++)
                {
                    _spawnablePositions.Planes[i].XYIndices[j] = new int[throwLevelRules.ySize];
                    for (var k = 0; k < throwLevelRules.ySize; k++)
                    {
                        _spawnablePositions.Planes[i].XYIndices[j][k] = index++;
                    }
                }
            }

            var posRotScls = new List<PosRotScl>();
            var usedIndices = new List<int>();

            for (var i = 0; i < throwLevelRules.stages; i++)
            {
                var plane = _spawnablePositions.Planes[i];

                for (var j = 0; j < throwLevelRules.targetsPerStage; j++)
                {
                    var isBig = Random.value < throwLevelRules.chanceBigTarget;
                    int index;
                    bool newLoop;

                    do
                    {
                        newLoop = false;
                        var x = Random.Range(0, throwLevelRules.xSize);
                        var y = Random.Range(0, throwLevelRules.ySize);

                        index = plane.XYIndices[x][y];
                        
                        // TODO - Check if a big target can be placed, turn into not big if true
                        if (!isBig) continue;

                        var isLegalBigStartIndex =
                            index >= throwLevelRules.xSize - 1 || index >= throwLevelRules.ySize - 1;

                        if (isLegalBigStartIndex)
                        {
                            newLoop = true;
                            continue;
                        }

                        var isAnIndexTaken =
                            usedIndices.Contains(index)
                            || usedIndices.Contains(plane.XYIndices[x + 1][y])
                            || usedIndices.Contains(plane.XYIndices[x][y + 1])
                            || usedIndices.Contains(plane.XYIndices[x + 1][y + 1]);

                        if (isAnIndexTaken)
                        {
                            newLoop = true;
                            continue;
                        }

                        usedIndices.AddRange(new[]
                        {
                            plane.XYIndices[x + 1][y],
                            plane.XYIndices[x][y + 1],
                            plane.XYIndices[x + 1][y + 1]
                        });
                        break;
                    } while (usedIndices.Contains(index) || newLoop);

                    usedIndices.Add(index);

                    posRotScls.Add(new PosRotScl
                    {
                        // ReSharper disable once PossibleLossOfFraction
                        location = new Vector3(
                            (index / throwLevelRules.xSize) * 0.75f - throwLevelRules.xSize * 0.75f / 2 + 0.75f * 0.5f,
                            (index % throwLevelRules.ySize) * 0.75f - throwLevelRules.ySize * 0.75f / 2 + 0.75f * 0.5f,
                            i * throwLevelRules.distBetweenStages),
                        rotation = new Vector3(-90, 0, 0)
                    });
                }
            }

            StartCoroutine(SetStageCoroutine(posRotScls));

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