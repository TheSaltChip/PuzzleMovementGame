using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level.Completables
{
    [Serializable]
    public class CompletableCollection : Completable
    {
        [SerializeReference] private List<Completable> items;

        private bool _isDone;

        public CompletableCollection(List<Completable> items)
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

            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].IsDone()) continue;

                _isDone = false;
                break;
            }

            return _isDone;
        }

        public void ResetList()
        {
            foreach (var item in items)
            {
                item.ResetState();
            }
        }
    }
}