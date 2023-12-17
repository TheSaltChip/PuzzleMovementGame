using System;
using UnityEngine;
using UnityEngine.Events;

namespace Memorization.Figure
{
    public class FigureInfo : MonoBehaviour
    {
        public string shapeName;
        public Color color;

        [SerializeField] private MeshCollider meshCollider;

        public UnityEvent<FigureInfo> onAddedToSelected;

        public bool Destroyed { get; private set; }

        public void AddToSelected()
        {
            if (Destroyed)
            {
                return;
            }

            onAddedToSelected?.Invoke(this);
        }

        public void DestroySelf()
        {
            Destroyed = true;
            meshCollider.enabled = false;
            Destroy(gameObject);
        }

        public bool Equals(FigureInfo obj, MatchingRule rule)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return rule switch
            {
                MatchingRule.Color => color.Equals(obj.color),
                MatchingRule.Figure => shapeName == obj.shapeName,
                _ => shapeName == obj.shapeName && color.Equals(obj.color)
            };
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), shapeName, color);
        }
    }
}