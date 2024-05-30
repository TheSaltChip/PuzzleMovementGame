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
using Autohand;
using Completables;
using UnityEngine;

namespace PatternRecognition
{
    [RequireComponent(typeof(PhysicsGadgetButton), typeof(MeshRenderer))]
    public class ColorButtonCompletable : Completable
    {
        [SerializeField] private PhysicsGadgetButton button;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Color correct;
        [SerializeField] private Color incorrect;
        private Color _dimmed;
        private Color _lit;

        private Material _material;

        private void Awake()
        {
            _material = meshRenderer.material;

            _lit = _material.color;

            Color.RGBToHSV(_material.color, out var h, out var s, out _);

            _dimmed = Color.HSVToRGB(h, s, 0.5f);

            _material.color = _dimmed;
        }

        private void OnEnable()
        {
            button.OnPressed.AddListener(Pressed);
            button.OnUnpressed.AddListener(TurnOff);
        }

        private void OnDisable()
        {
            button.OnPressed.RemoveListener(Pressed);
            button.OnUnpressed.RemoveListener(TurnOff);
        }

        public IEnumerator Blink(float duration)
        {
            yield return StartCoroutine(BlinkColor(duration, _lit, _dimmed));
        }

        public IEnumerator BlinkCorrectColor(float duration)
        {
            yield return StartCoroutine(BlinkColor(duration, correct, _dimmed));
        }

        public IEnumerator BlinkIncorrectColor(float duration)
        {
            yield return StartCoroutine(BlinkColor(duration, incorrect, _dimmed));
        }

        private IEnumerator BlinkColor(float duration, Color start, Color end)
        {
            _material.color = start;

            yield return new WaitForSeconds(duration);

            _material.color = end;
        }

        private void Pressed()
        {
            TurnOn();
            Completed();
        }

        private void TurnOn()
        {
            _material.color = _lit;
        }

        private void TurnOff()
        {
            _material.color = _dimmed;
        }
    }
}