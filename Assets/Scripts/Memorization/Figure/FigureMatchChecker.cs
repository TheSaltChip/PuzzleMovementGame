using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Memorization.Figure.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Memorization.Figure
{
    public class FigureMatchChecker : MonoBehaviour
    {
        [SerializeField] private FigureMatchingRules rules;

        private List<FigureInfo> _list;

        public UnityEvent onCorrectMatch;
        public UnityEvent onIncorrectMatch;

        private void Awake()
        {
            _list = new List<FigureInfo>(rules.numToMatch);
        }

        public void Add(FigureInfo item)
        {
            if (item == null)
            {
                print("NULL");
                return;
            }

            if (item.destroyed)
            {
                print("DESTROYED");
                return;
            }
            
            if (_list.Contains(item))
            {
                print("CONTAINS");

                var sb = new StringBuilder();
                foreach (var figureInfo in _list)
                {
                    sb.Append(figureInfo.shapeName + " ");
                }
                
                print(sb.ToString());
                
                return;
            }

            _list.Add(item);
            
            if (_list.Count == rules.numToMatch)
            {
                Matches();
            }
        }

        public void Matches()
        {
            var item = _list[0];

            for (var i = 1; i < _list.Count; i++)
            {
                if (!item.Equals(_list[i], rules.matchingRule))
                {
                    ReenableColliderAndClear();
                    onIncorrectMatch?.Invoke();
                    return;
                }

                item = _list[i];
            }

            DeleteAndClear();
            onCorrectMatch?.Invoke();
        }

        private void DeleteAndClear()
        {
            var list = new List<FigureInfo>(_list);
            _list.Clear();
            
            foreach (var figureInfo in list)
            {
                if (figureInfo == null)
                {
                    print("Destroyed?");
                    continue;
                }
                figureInfo.DestroySelf();
            }
        }

        private void ReenableColliderAndClear()
        {
            _list.Clear();
        }

    }
}