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

        public bool destroyed;

        public void AddToSelected()
        {
            if (destroyed)
            {
                print("Is destroyed");
                return;
            }

            onAddedToSelected?.Invoke(this);
        }

        public void DestroySelf()
        {
            destroyed = false;
            Destroy(gameObject);
        }

        public void EnableCollider()
        {
            meshCollider.enabled = true;
        }

        public void DisableCollider()
        {
            meshCollider.enabled = false;
        }

        private bool Equals(FigureInfo other, MatchingRule rule)
        {
            return rule switch
            {
                MatchingRule.Color => color.Equals(other.color),
                MatchingRule.Figure => shapeName == other.shapeName,
                _ => shapeName == other.shapeName && color.Equals(other.color)
            };
        }

        public bool Equals(object obj, MatchingRule rule)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals((FigureInfo)obj, rule);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), shapeName, color);
        }
    }
}