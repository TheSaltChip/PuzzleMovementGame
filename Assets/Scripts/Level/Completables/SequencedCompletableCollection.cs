using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Completables
{
    [Serializable]
    public class SequencedCompletableCollection : Completable
    {
        [SerializeReference] private List<Completable> items;

        private bool _isDone;
        private int _completedIndex;

        public SequencedCompletableCollection(List<Completable> items)
        {
            this.items = items;
        }

        public override bool IsDone()
        {
            return _isDone;
        }

        public override void ResetState()
        {
            foreach (var completable in items)
            {
                completable.ResetState();
            }
        }

        public bool CheckCompletion()
        {
            _isDone = true;

            for (var i = _completedIndex; i < items.Count; i++)
            {
                if (items[i].IsDone())
                {
                    _completedIndex = i;
                    continue;
                }

                _isDone = false;

                CheckRestOfList(i);

                break;
            }

            return _isDone;
        }

        private void CheckRestOfList(int start)
        {
            for (var j = start; j < items.Count; j++)
            {
                if (!items[j].IsDone()) continue;

                ResetList();
                break;
            }
        }

        public void ResetList()
        {
            _completedIndex = 0;
            foreach (var item in items)
            {
                item.ResetState();
            }
        }
    }
}