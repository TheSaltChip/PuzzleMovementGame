using System;
using ThrowingOnTargets.Saveable;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Serialization;

namespace ThrowingOnTargets.Difficulty
{
    public class DifficultySetter : MonoBehaviour
    {
        [SerializeField] private ThrowLevelSO throwLevel;
        [SerializeField] private TargetInfo targetInfo;

        public void SetDifficulty(ThrowLevelDifficulty difficulty)
        {
            switch (difficulty)
            {
                case ThrowLevelDifficulty.Easy:
                    CreateStage(3, 4, 6);
                    break;
                case ThrowLevelDifficulty.Medium:
                    CreateStage(5, 6, 10);
                    break;
                case ThrowLevelDifficulty.Hard:
                    CreateStage(7, 8, 13);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null);
            }
        }

        public void SetEasyDifficulty()
        {
            SetDifficulty(ThrowLevelDifficulty.Easy);
        }

        private const float LengthBetweenClosestTargets = 0.65f;

        // ReSharper disable InconsistentNaming
        private const float LengthBetweenCenterAnd2ndRing = 2 * LengthBetweenClosestTargets;
        private const float LengthBetweenCenterAnd3rdRing = 3 * LengthBetweenClosestTargets;
        // ReSharper restore InconsistentNaming

        // (x: cos(Length), y: sin(Length))

        /*
         * 1: 1
         * 2: 6 - 60° per target
         * 3: 12 - 30° per target
         * 4: 24 - 15° per target
         */

        private void CreateStage(int numStages, int minTargets, int maxTargets)
        {
            var stages = new Stage[numStages];

            var center = targetInfo.spawnPoint;

            var ring1 = (int)(6 * Mathf.Pow(2, 0));
            var ring2 = (int)(6 * Mathf.Pow(2, 1));
            var ring3 = (int)(6 * Mathf.Pow(2, 2));
            var places = new Vector3[1 + ring1 + ring2];


            places[0] = center;
            
            for (var i = 0; i < ring1; i++)
            {
                places[i+1] = center +
                            new Vector3(
                                LengthBetweenClosestTargets * Mathf.Cos(i * Mathf.PI / 3f),
                                LengthBetweenClosestTargets * Mathf.Sin(i * Mathf.PI / 3f),
                                0);
            }

            var step = 0;

            var firstPointRing2 = center;
            
            for (var i = 0; i < ring2; i++)
            {
                var secondPoint =firstPointRing2 +
                                  new Vector3(
                                      LengthBetweenClosestTargets * Mathf.Cos(step * Mathf.PI / 3f),
                                      LengthBetweenClosestTargets * Mathf.Sin(step * Mathf.PI / 3f),
                                      0);
                
                firstPointRing2 = secondPoint;
                
                if (i != 0 && i % 2 == 0)
                {
                    ++step;
                }

                places[ring1 + i + 1] = center + secondPoint;
                
            }

            var firstPointRing3 = new Vector3(
                LengthBetweenCenterAnd3rdRing,
                LengthBetweenCenterAnd3rdRing,
                0);

            print(ring3 - ring2 - ring1);
            print(ring1);
            print(ring2);
            print(ring3);
            /*for (var i = 0; i < ring3; i++)
            {
                var secondPoint = firstPointRing3 +
                                  new Vector3(
                                      LengthBetweenClosestTargets * Mathf.Cos(step * Mathf.PI / 3f),
                                      LengthBetweenClosestTargets * Mathf.Sin(step * Mathf.PI / 3f),
                                      0);

                if (i != 0 && i % 3 == 0)
                {
                    ++step;
                    firstPointRing3 = secondPoint;
                }
                
                places[ring2 + i] = center + secondPoint;
            }*/
            
            var stage = new Stage { posRots = new PosRotScl[places.Length] };

            for (var i = 0; i < places.Length; i++)
            {
                stage.posRots[i] = new PosRotScl()
                {
                    location = places[i],
                    rotation = new Vector3(90, 0, 180),
                    scale = Vector3.one
                };
            }


            throwLevel.stages = new[]
            {
                stage
            };
        }
    }

    public enum ThrowLevelDifficulty
    {
        Easy,
        Medium,
        Hard
    }
}