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

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FigureMatching
{
    public class FigureInfo : MonoBehaviour
    {
        public string shapeName;
        public Color color;
        public bool Destroyed { get; private set; }

        [SerializeField] private MeshCollider meshCollider;
        [SerializeField] private Material outlineMaterial;

        public UnityEvent<FigureInfo> onAddedToSelected;

        private bool _deactivated;
        private readonly WaitForSeconds _waitForSeconds = new(0.5f);

        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = gameObject.GetComponentInChildren<MeshRenderer>();
        }

        public void AddToSelected()
        {
            if (Destroyed || _deactivated)
            {
                return;
            }

            onAddedToSelected?.Invoke(this);
        }

        public void Deactivate()
        {
            _deactivated = true;
            AddOutline();
        }

        public void Activate()
        {
            RemoveOutline();
            StartCoroutine(ActivateCoroutine());
        }

        private IEnumerator ActivateCoroutine()
        {
            yield return _waitForSeconds;

            _deactivated = false;
        }

        private void AddOutline()
        {
            var material = _renderer.material;

            var newM = new[] { material, outlineMaterial };

            _renderer.materials = newM;
        }

        private void RemoveOutline()
        {
            _renderer.materials = new[] { _renderer.material };
        }

        public void DestroySelf()
        {
            Destroyed = true;
            meshCollider.enabled = false;
            Destroy(gameObject);
        }

        public bool Equals(FigureInfo obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return shapeName == obj.shapeName && color.Equals(obj.color);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), shapeName, color);
        }
    }
}