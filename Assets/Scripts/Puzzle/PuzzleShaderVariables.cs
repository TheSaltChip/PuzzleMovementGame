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