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