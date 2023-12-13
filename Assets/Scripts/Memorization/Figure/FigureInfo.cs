using System;
using Memorization.Figure.ScriptableObjects;
using UnityEngine;

namespace Memorization.Figure
{
    public class FigureInfo : MonoBehaviour
    {
        public string shapeName;
        public Color color;

        public SelectedFigures sel;

        [SerializeField] private MeshCollider meshCollider;

        public void AddToSelected()
        {
            if (sel.Contains(this)) return;

            sel.Add(this);
            meshCollider.enabled = false;
        }

        public void EnableCollider()
        {
            meshCollider.enabled = true;
        }

        private bool Equals(FigureInfo other, MatchingRule rule)
        {
            return rule switch
            {
                MatchingRule.Color => base.Equals(other) && color.Equals(other.color),
                MatchingRule.Figure => base.Equals(other) && shapeName == other.shapeName,
                _ => base.Equals(other) && shapeName == other.shapeName && color.Equals(other.color)
            };
        }

        public bool Equals(object obj, MatchingRule rule)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (rule == MatchingRule.Color) return obj.GetType() == GetType() && Equals((FigureInfo)obj, rule);

            return obj.GetType() == GetType() && Equals((FigureInfo)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), shapeName, color);
        }
    }
}