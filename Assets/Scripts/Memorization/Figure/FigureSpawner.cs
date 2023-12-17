using System;
using System.Collections.Generic;
using Memorization.Figure.ScriptableObjects;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace Memorization.Figure
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField] private Vector3 center;

        [SerializeField] private FigureMatchingRules rules;

        [SerializeField] private Figures figures;
        [SerializeField] private FigureMaterials materials;
        [SerializeField] private FigurePositions positions;

        public void Spawn()
        {
            switch (rules.matchingRule)
            {
                case MatchingRule.Color:
                    ColorSpawn();
                    break;
                case MatchingRule.Figure:
                    FigureSpawn();
                    break;
                case MatchingRule.FigureAndColor:
                    FigureAndColorSpawn();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void FigureAndColorSpawn()
        {
            var posCopy = positions.Copy();
            posCopy.Shuffle();

            var figs = figures.RandomFigures(rules.maxNumFigure);
            var mats = materials.RandomMaterials(rules.maxNumColor);

            for (var i = 0; i < rules.TotalTotalNumberOfFigures; i += rules.numToMatch)
            {
                var fig = figs[Random.Range(0, figs.Count)];
                var mat = mats[Random.Range(0, mats.Count)];

                CreateFigures(fig, mat, posCopy, i);
            }
        }

        private void FigureSpawn()
        {
            var posCopy = positions.Copy();
            posCopy.Shuffle();

            var figs = figures.RandomFigures(rules.maxNumFigure);

            var mat = materials.RandomObject();

            for (var i = 0; i < rules.TotalTotalNumberOfFigures; i += rules.numToMatch)
            {
                var fig = figs[Random.Range(0, figs.Count)];

                CreateFigures(fig, mat, posCopy, i);
            }
        }

        private void ColorSpawn()
        {
            var posCopy = positions.Copy();
            posCopy.Shuffle();

            var mats = materials.RandomMaterials(rules.maxNumColor);

            var fig = figures.RandomObject();

            for (var i = 0; i < rules.TotalTotalNumberOfFigures; i += rules.numToMatch)
            {
                var mat = mats[Random.Range(0, mats.Count)];

                CreateFigures(fig, mat, posCopy, i);
            }
        }

        private void CreateFigures(GameObject fig, Material mat, IReadOnlyList<Vector3> posCopy, int i)
        {
            var p1 = Instantiate(fig, transform);
            p1.GetComponentInChildren<MeshRenderer>().material = mat;
            p1.transform.localPosition = posCopy[i];
            p1.transform.LookAt(transform);

            var figureInfo = p1.GetComponent<FigureInfo>();
            figureInfo.color = mat.color;
            figureInfo.shapeName = fig.name;

            for (var j = i + 1; j < i + rules.numToMatch; j++)
            {
                var p2 = Instantiate(p1, transform);
                p2.transform.localPosition = posCopy[j];
                p2.transform.LookAt(transform);
            }
        }
    }
}