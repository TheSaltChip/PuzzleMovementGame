using ThrowingOnTargets.Saveable;
using ThrowingOnTargets.ScriptableObjects;
using UnityEngine;
using Util.PRS;

namespace ThrowingOnTargets
{
    public class ThrowAtTargetGridCreator : MonoBehaviour
    {
        [SerializeField] private TargetInfo targetInfo;
        [SerializeField, Min(0)] private float padding;
        
        private float LengthBetweenClosestTargets => 0.5f + padding;

        public void CreateGrid(int n)
        {
            var ringSum = 1 + 6 * (n * (n + 1) / 2);

            var ringNum = 0;
            var targetNum = 0;

            var places = new Vector3[ringSum];
            var point = Vector3.zero;

            var step = 1;

            for (var i = 1; i < ringSum; ++i, ++targetNum)
            {
                if (i == 1 || CheckI(i, n))
                {
                    point = new Vector3(LengthBetweenClosestTargets * ringNum, 0, 0);
                    ++ringNum;
                    targetNum = 0;
                    step = 0;
                }

                if (step == 1)
                {
                    step = 2;
                }

                point += new Vector3(
                    LengthBetweenClosestTargets * Mathf.Cos(step * Mathf.PI / 3f),
                    LengthBetweenClosestTargets * Mathf.Sin(step * Mathf.PI / 3f),
                    0);

                if (targetNum % ringNum == 0)
                {
                    ++step;
                }

                places[i] = point;
            }

            targetInfo.grid = places;
            
            var stage = new Stage { posRots = new PosRotScl[places.Length] };

            for (var i = 0; i < places.Length; i++)
            {
                stage.posRots[i] = new PosRotScl()
                {
                    position = places[i],
                    rotation = new Vector3(90, 0, 180),
                    scale = Vector3.one
                };
            }
        }
        
        private bool CheckI(int i, int n)
        {
            for (var j = 1; j <= n; j++)
            {
                if (i == 1 + 6 * (j * (j + 1) / 2)) return true;
            }

            return false;
        }
    }
}