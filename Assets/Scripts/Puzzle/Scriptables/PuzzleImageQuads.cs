using System;
using UnityEngine;

namespace Puzzle.Scriptables
{
    [CreateAssetMenu(fileName = "PuzzleImageQuads", menuName = "Puzzle/PuzzleImageQuads", order = 0)]
    public class PuzzleImageQuads : ScriptableObject
    {
        public Quad[] quads;
        public Quad[] rearrangedQuads;
    }

    [Serializable]
    public struct Quad
    {
        public int[,] rows;
    }
}