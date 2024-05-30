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

using System.Collections;
using UnityEngine;
using Variables;

namespace SceneTransition
{
    [CreateAssetMenu(menuName = "XRRig/FaderScreen", fileName = "FaderScreen")]
    public class FaderScreen : ScriptableObject
    {
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        [SerializeField] private FloatVariable animationTime;
        [SerializeField] private Material material;

        private void Awake()
        {
            material.SetColor(Color1, Color.clear);
        }

        public IEnumerator FadeRoutine(Color from, Color to)
        {
            var time = 0f;

            while (time <= animationTime.value)
            {
                material.SetColor(Color1, Color.Lerp(
                    from,
                    to,
                    time / animationTime.value
                ));

                time += Time.deltaTime;

                yield return null;
            }

            material.SetColor(Color1, to);
        }
    }
}