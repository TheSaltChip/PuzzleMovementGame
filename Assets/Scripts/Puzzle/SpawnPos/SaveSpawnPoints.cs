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