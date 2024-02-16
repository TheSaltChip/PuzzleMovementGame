using UnityEngine;

namespace Puzzle
{
    public static class PuzzleShaderVariables
    {
        public static readonly int BaseMap = Shader.PropertyToID("_BaseMap");
        public static readonly int Image = Shader.PropertyToID("image");
        public static readonly int Ri = Shader.PropertyToID("ri"); //render image in compute shader
        public static readonly int Result = Shader.PropertyToID("Result");
        public static readonly int Quads = Shader.PropertyToID("quads");
        public static readonly int RearrangedQuads = Shader.PropertyToID("rearrangedQuads");
        public static readonly int Width = Shader.PropertyToID("width");
        public static readonly int Height = Shader.PropertyToID("height");
        public static readonly int PixelCount = Shader.PropertyToID("pixelCount");
    }
}