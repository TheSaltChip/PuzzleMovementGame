using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Util;
using Variables;
using Random = UnityEngine.Random;

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

        public void SetupStage()
        {
            _spawnablePositions = new Depths
            {
                Planes = new Plane[throwLevelRules.Stages]
            };

            for (var i = 0; i < throwLevelRules.Stages; i++)
            {
                _spawnablePositions.Planes[i].XYIndices = new int[throwLevelRules.XSize][];
                var index = 0;

                for (var j = 0; j < throwLevelRules.XSize; j++)
                {
                    _spawnablePositions.Planes[i].XYIndices[j] = new int[throwLevelRules.YSize];
                    for (var k = 0; k < throwLevelRules.YSize; k++)
                    {
                        _spawnablePositions.Planes[i].XYIndices[j][k] = index++;
                    }
                }
            }

            var posRotScls = new List<PosRotScl>();
            var usedIndices = new List<int>();
            var upperIndexCountLimit = throwLevelRules.XSize * throwLevelRules.YSize;
            
            for (var i = 0; i < throwLevelRules.Stages; i++)
            {
                var plane = _spawnablePositions.Planes[i];
                usedIndices.Clear();

                for (var j = 0; j < throwLevelRules.TargetsPerStage; j++)
                {
                    var isBig = Random.value <= throwLevelRules.ChanceBigTarget;

                    while (usedIndices.Count < upperIndexCountLimit)
                    {
                        var x = Random.Range(0, throwLevelRules.XSize);
                        var y = Random.Range(0, throwLevelRules.YSize);

                        var index = plane.XYIndices[x][y];

                        if (usedIndices.Contains(index)) continue;

                        if (!isBig)
                        {
                            usedIndices.Add(index);
                            posRotScls.Add(new PosRotScl
                            {
                                // ReSharper disable once PossibleLossOfFraction
                                location = new Vector3(
                                    x * 0.75f
                                    - throwLevelRules.XSize * 0.75f / 2
                                    + 0.75f * 0.5f,
                                    y * 0.75f
                                    - throwLevelRules.YSize * 0.75f / 2
                                    + 0.75f * 0.5f,
                                    i * throwLevelRules.DistBetweenStages),
                                rotation = new Vector3(-90, 0, 0),
                                scale = new Vector3(1, 1, 1)
                            });
                            break;
                        }

                        if (!IsTherePlaceForBig(plane, usedIndices))
                        {
                            isBig = false;
                            continue;
                        }

                        var isIllegalBigStartIndex =
                            x >= throwLevelRules.XSize - 1 ||
                            y >= throwLevelRules.YSize - 1;

                        if (isIllegalBigStartIndex)
                        {
                            continue;
                        }

                        var isTheRestOfIndicesTaken =
                            usedIndices.Contains(plane.XYIndices[x + 1][y])
                            || usedIndices.Contains(plane.XYIndices[x][y + 1])
                            || usedIndices.Contains(plane.XYIndices[x + 1][y + 1]);

                        if (isTheRestOfIndicesTaken)
                        {
                            continue;
                        }

                        usedIndices.AddRange(new[]
                        {
                            index,
                            plane.XYIndices[x + 1][y],
                            plane.XYIndices[x][y + 1],
                            plane.XYIndices[x + 1][y + 1]
                        });

                        posRotScls.Add(new PosRotScl
                        {
                            // ReSharper disable once PossibleLossOfFraction
                            location = new Vector3(
                                x * 0.75f
                                - throwLevelRules.XSize * 0.75f / 2
                                + 0.75f * 0.5f + 0.75f * 0.5f,
                                y * 0.75f
                                - throwLevelRules.YSize * 0.75f / 2
                                + 0.75f * 0.5f + 0.75f * 0.5f,
                                i * throwLevelRules.DistBetweenStages),
                            rotation = new Vector3(-90, 0, 0),
                            scale = new Vector3(1.5f, 1, 1.5f)
                        });
                    }
                }
            }

            StartCoroutine(SetStageCoroutine(posRotScls));
            onStageReady?.Invoke();
        }

        private static bool IsTherePlaceForBig(Plane plane, ICollection<int> usedIndices)
        {
            for (var x = 0; x < plane.XYIndices.Length - 1; x++)
            {
                for (var y = 0; y < plane.XYIndices[x].Length - 1; y++)
                {
                    if (!usedIndices.Contains(plane.XYIndices[x][y])
                        && !usedIndices.Contains(plane.XYIndices[x + 1][y])
                        && !usedIndices.Contains(plane.XYIndices[x][y + 1])
                        && !usedIndices.Contains(plane.XYIndices[x + 1][y + 1]))
                    {
                        return true;
                    }
                }
            }

            return false;
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
                t.transform.localScale = posRotScl[i].scale;
            }
        }

        private void OnDrawGizmos()
        {
            for (var i = 0; i < throwLevelRules.Stages; i++)
            {
                for (var x = 0; x < throwLevelRules.XSize; x++)
                {
                    for (var y = 0; y < throwLevelRules.YSize; y++)
                    {
                        Gizmos.DrawWireCube(transform.position + new Vector3(
                            x * 0.75f - throwLevelRules.XSize * 0.75f / 2 + 0.75f * 0.5f,
                            y * 0.75f - throwLevelRules.YSize * 0.75f / 2 + 0.75f * 0.5f,
                            i * throwLevelRules.DistBetweenStages), Vector3.one * 0.75f);
                    }
                }
            }
        }
    }
}