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