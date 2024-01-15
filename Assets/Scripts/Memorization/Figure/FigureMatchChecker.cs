using System.Collections.Generic;
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
        public UnityEvent onAddedToList;

        private void Awake()
        {
            _list = new List<FigureInfo>(rules.NumToMatch);
        }

        public void Add(FigureInfo item)
        {
            if (_list.Contains(item)) return;

            _list.Add(item);
            onAddedToList?.Invoke();
            item.Deactivate();
            
            if (_list.Count == rules.NumToMatch)
            {
                Matches();
            }
        }

        public void Matches()
        {
            var item = _list[0];

            for (var i = 1; i < _list.Count; i++)
            {
                if (!item.Equals(_list[i]))
                {
                    ActivateAndClear();
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
                figureInfo.DestroySelf();
            }
        }

        private void ActivateAndClear()
        {
            foreach (var figureInfo in _list)
            {
                figureInfo.Activate();
            }
            _list.Clear();
        }

        public void ClearList()
        {
            _list.Clear();
        }
    }
}