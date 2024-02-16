using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util.PRS;

namespace Puzzle.SpawnPos
{
    [CreateAssetMenu(fileName = "SpawnPoints", menuName = "Puzzle/SpawnPoints")]
    public class PuzzlePiecesSpawnPoints : ScriptableObject, IEnumerable<List<PosRotScl>>
    {
        public List<PosRotScl> layers1;
        public List<PosRotScl> layers2;
        public List<PosRotScl> layers3;
        
        public List<PosRotScl> GetLayerCopy(int layerIndex)
        {
            if (layerIndex is > 2 or < 0)
            {
                return new List<PosRotScl>();
            }

            return layerIndex switch
            {
                0 => new List<PosRotScl>(layers1),
                1 => new List<PosRotScl>(layers2),
                2 => new List<PosRotScl>(layers3),
                _ => throw new ArgumentOutOfRangeException(nameof(layerIndex), layerIndex, null)
            };
        }

        public void AddLayer(int layerIndex, List<PosRotScl> prs)
        {
            switch (layerIndex)
            {
                case 0:
                    layers1 = prs;
                    break;
                case 1:
                    layers2 = prs;
                    break;
                case 2:
                    layers3 = prs;
                    break;
            }
        }

        public IEnumerator<List<PosRotScl>> GetEnumerator()
        {
            yield return layers1;
            yield return layers2;
            yield return layers3;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}