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

using FigureMatching.ScriptableObjects;
using UnityEngine;
using Util;

namespace FigureMatching
{
    public class FigureSpawner : MonoBehaviour
    {
        private Transform _spawnPoint;

        [SerializeField] private FigureMatchingRules rules;

        [SerializeField] private Figures figures;
        [SerializeField] private FigureMaterials materials;
        [SerializeField] private FigurePositions positions;

        private readonly Vector3 _adjustToNeckHeight = new(0, 0.20f, 0);

        public void SetSpawnPosition()
        {
            if (_spawnPoint == null)
            {
                var mainCamera = GameObject.FindWithTag("MainCamera");

                _spawnPoint = mainCamera == null ? transform : mainCamera.transform;
            }

            transform.position = _spawnPoint.transform.position - _adjustToNeckHeight;
        }

        public void Spawn()
        {
            var posCopy = positions.Copy();
            posCopy.Shuffle();

            var shapes = figures.RandomFigures(rules.MaxNumShapes);
            var mats = materials.RandomMaterials(rules.MaxNumColor);

            for (var i = 0; i < rules.TotalTotalNumberOfFigures; i += rules.NumToMatch)
            {
                var fig = shapes[Random.Range(0, shapes.Count)];
                var mat = mats[Random.Range(0, mats.Count)];

                var p1 = Instantiate(fig, transform);
                p1.GetComponentInChildren<MeshRenderer>().material = mat;
                p1.transform.localPosition = posCopy[i];
                p1.transform.LookAt(transform);

                var figureInfo = p1.GetComponent<FigureInfo>();
                figureInfo.color = mat.color;
                figureInfo.shapeName = fig.name;

                for (var j = i + 1; j < i + rules.NumToMatch; j++)
                {
                    var p2 = Instantiate(p1, transform);
                    p2.transform.localPosition = posCopy[j];
                    p2.transform.LookAt(transform);
                }
            }
        }
    }
}