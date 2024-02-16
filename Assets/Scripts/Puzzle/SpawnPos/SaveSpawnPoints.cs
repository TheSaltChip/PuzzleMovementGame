using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using Util.PRS;

namespace Puzzle.SpawnPos
{
    public class SaveSpawnPoints : MonoBehaviour
    {
        [SerializeField] private PuzzlePiecesSpawnPoints spawnPoints;
        [SerializeField] private GameObject rootSpawnPoints;

        public void SavePoints()
        {
            var layers = new List<GameObject>(rootSpawnPoints.transform.childCount);

            rootSpawnPoints.GetChildGameObjects(layers);

            for (var i = 0; i < layers.Count; i++)
            {
                var layer = layers[i];
                var posInLayer = new List<GameObject>();
                layer.GetChildGameObjects(posInLayer);

                var temp = new List<PosRotScl>(posInLayer.Count);
                
                temp.AddRange(posInLayer.Select(t => t.transform.ToPosRotScl()));

                spawnPoints.AddLayer(i, temp);
            }
        }

        private void OnDrawGizmos()
        {
            var size = Vector3.one * 0.1f;
            foreach (List<PosRotScl> layer in spawnPoints)
            foreach (PosRotScl pos in layer)
            {
                Gizmos.DrawWireCube(pos.position, size);
            }
        }
    }
}