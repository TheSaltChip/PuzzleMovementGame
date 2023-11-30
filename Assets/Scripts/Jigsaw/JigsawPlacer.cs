using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Jigsaw
{
    public class JigsawPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject piecesParent;
        [SerializeField] private GameObject spawnPointParent;

        private Transform[] _pieces;
        private Transform[] _spawnPoints;

        private void Awake()
        {
            _pieces = piecesParent.GetComponentsInChildren<Transform>();
            _pieces = _pieces.Skip(1).ToArray();

            _spawnPoints = spawnPointParent.GetComponentsInChildren<Transform>();

            _spawnPoints = _spawnPoints.Skip(1).ToArray();


            _spawnPoints.Shuffle();

            for (var i = 0; i < _pieces.Length; i++)
            {
                _pieces[i].position = _spawnPoints[i%(_spawnPoints.Length)].position;
            }
        }
    }

    public static class Test
    {
        private static readonly Random Rng = new();

        public static void Shuffle<T>(this T[] list)
        {
            var n = list.Length;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}