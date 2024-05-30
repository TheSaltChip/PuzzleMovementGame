#region License
// Copyright (C) 2024 Sebastian Misje Jonassen & Mathias Nupen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Commons Clause License version 1.0 with GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Commons Clause License and GNU General Public License for more details.
// 
// You should have received a copy of the Commons Clause License and GNU General Public License
// along with this program.  If not, see <https://commonsclause.com/> and <https://www.gnu.org/licenses/>.
#endregion

using System.Collections;
using System.Collections.Generic;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using Util;
using Util.PRS;
using Variables;

namespace ThrowingOnTargets
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField] private ThrowLevelRules throwLevelRules;
        [SerializeField] private IntVariable targetsInLevel;

        [SerializeField] private GameObject target;

        public UnityEvent onLevelReady;

        private IObjectPool<GameObject> _targets;
        private Coroutine _setupCoroutine;
        private WaitForSeconds _waitFor50Milliseconds;

        private struct Plane
        {
            public int[][] XYIndices;
        }

        private struct Depths
        {
            public Plane[] Planes;
        }

        private Depths _spawnablePositions;

        private void Awake()
        {
            _targets = new ObjectPool<GameObject>(() =>
            {
                var go = Instantiate(target, transform, true);

                if (go.TryGetComponent<ReturnToPool>(out var rtp))
                    rtp.pool = _targets;

                return go;
            }, o => o.SetActive(true), obj => obj.SetActive(false), Destroy);
            _waitFor50Milliseconds = new WaitForSeconds(0.05f);
        }

        public void SetupLevel()
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

            var upperIndexCountLimit = throwLevelRules.XSize * throwLevelRules.YSize;
            var posRotScls = new List<PosRotScl>(upperIndexCountLimit * throwLevelRules.Stages);
            var usedIndices = new List<int>();

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
                                position = new Vector3(
                                    x * 0.75f - throwLevelRules.XSize * 0.375f + 0.375f,
                                    y * 0.75f - throwLevelRules.YSize * 0.375f + 0.375f,
                                    i * throwLevelRules.DistBetweenStages),
                                rotation = new Vector3(-90, 0, 0),
                                scale = Vector3.one
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
                            position = new Vector3(
                                x * 0.75f - throwLevelRules.XSize * 0.375f + 0.75f,
                                y * 0.75f - throwLevelRules.YSize * 0.375f + 0.75f,
                                i * throwLevelRules.DistBetweenStages),
                            rotation = new Vector3(-90, 0, 0),
                            scale = new Vector3(1.5f, 1, 1.5f)
                        });
                        break;
                    }
                }
            }

            targetsInLevel.value = posRotScls.Count;

            StartCoroutine(SetupLevelCoroutine(posRotScls));
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

        private IEnumerator SetupLevelCoroutine(IList<PosRotScl> posRotScl)
        {
            for (var i = 0; i < posRotScl.Count; i++)
            {
                yield return _waitFor50Milliseconds;
                var t = _targets.Get();

                t.transform.SetLocalPositionAndRotation(
                    posRotScl[i].position,
                    Quaternion.Euler(posRotScl[i].rotation));
                t.transform.localScale = posRotScl[i].scale;
            }
            
            onLevelReady?.Invoke();
        }

        private void OnDrawGizmos()
        {
            var cubeSize = Vector3.one * 0.75f;

            for (var i = 0; i < throwLevelRules.Stages; i++)
            for (var x = 0; x < throwLevelRules.XSize; x++)
            for (var y = 0; y < throwLevelRules.YSize; y++)
            {
                Gizmos.DrawWireCube(transform.position + new Vector3(
                    x * 0.75f - throwLevelRules.XSize * 0.375f + 0.375f,
                    y * 0.75f - throwLevelRules.YSize * 0.375f + 0.375f,
                    i * throwLevelRules.DistBetweenStages), cubeSize);
            }
        }
    }
}