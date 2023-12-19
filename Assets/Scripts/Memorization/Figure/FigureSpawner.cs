using Memorization.Figure.ScriptableObjects;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

namespace Memorization.Figure
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;

        [SerializeField] private FigureMatchingRules rules;

        [SerializeField] private Figures figures;
        [SerializeField] private FigureMaterials materials;
        [SerializeField] private FigurePositions positions;
        
        private readonly Vector3 _adjustToNeckHeight = new(0, 0.15f, 0);
        
        public void Spawn()
        {
            transform.position =
                spawnPoint == null ? Vector3.zero : spawnPoint.transform.position - _adjustToNeckHeight;

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