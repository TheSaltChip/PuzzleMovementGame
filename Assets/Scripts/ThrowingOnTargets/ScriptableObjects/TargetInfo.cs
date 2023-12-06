using UnityEngine;

namespace ThrowingOnTargets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TargetInfo", menuName = "ThrowAtTargets/TargetInfo", order = 0)]
    public class TargetInfo : ScriptableObject
    {
        public Vector3 spawnPoint;
        
        public Vector3[] grid;

        public Vector3[] GridRing1 => GetRing(1);
        public Vector3[] GridRing1To2 => GetRing(2);

        private Vector3[] GetRing(int n)
        {
            var newArr = new Vector3[1 + 6 * (n * (n + 1) / 2)];

            for (var i = 0; i < newArr.Length; i++)
            {
                newArr[i] = grid[i];
            }

            return newArr;
        }
    }
}