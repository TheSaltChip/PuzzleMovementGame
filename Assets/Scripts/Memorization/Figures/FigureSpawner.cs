using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Memorization.Figures
{
    public class FigureSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Vector3 center;
        [SerializeField, Range(2,16)] private int amountOfFigures;

        [SerializeField] private FigureSpawnerSettings settings;
        [SerializeField] private FigureMatchingRules rules;

        [SerializeField] private Figures figures;
        [SerializeField] private FigureMaterials materials;

        private List<Vector3> _positions;

        private void Awake()
        {
            _positions = new List<Vector3>();
        }

        public void Spawn()
        {
            _positions.Clear();
            
            var deltaTheta = settings.deltaThetaDeg * Mathf.Deg2Rad;
            var deltaPhi = settings.deltaPhiDeg * Mathf.Deg2Rad;
            var radius = settings.radius;

            const float tau = 2f * Mathf.PI;
            const float piDiv2 = Mathf.PI / 2f;

            for (var theta = piDiv2; theta > Mathf.PI/6 + 1e-6f; theta -= deltaTheta)
            {
                for (var phi = 0f; phi < tau - 1e-6f; phi += deltaPhi)
                {
                    var pos = new Vector3(
                        radius * Mathf.Cos(phi) * Mathf.Sin(theta),
                        radius * Mathf.Cos(theta),
                        radius * Mathf.Sin(phi) * Mathf.Sin(theta));
                    pos += transform.position;
                    _positions.Add(pos);
                }
            }

            _positions.Shuffle();

            var posLength = _positions.Count;
            
            print(posLength);

            if (posLength % 2 == 1)
            {
                --posLength;
            }

            // TODO: Spawne inn basert på hvilken regel som er valgt
            // TODO: F.eks: Samme form hvis kun farge er valgt
            
            var numFigures = posLength / 2;

            for (var i = 0; i < posLength; i++)
            {
                var p =Instantiate(prefab, transform);
                p.transform.localPosition = _positions[i];
            }
        }
    }
}