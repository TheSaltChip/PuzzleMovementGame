﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Memorization.Figure
{
    public class FigureInfo : MonoBehaviour
    {
        public string shapeName;
        public Color color;
        public bool Destroyed { get; private set; }

        [SerializeField] private MeshCollider meshCollider;

        public UnityEvent<FigureInfo> onAddedToSelected;

        private bool _deactivated;
        private readonly WaitForSeconds _waitForSeconds = new(0.5f);


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
        }

        public void Activate()
        {
            StartCoroutine(ActivateCoroutine());
        }

        private IEnumerator ActivateCoroutine()
        {
            yield return _waitForSeconds;

            _deactivated = false;
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