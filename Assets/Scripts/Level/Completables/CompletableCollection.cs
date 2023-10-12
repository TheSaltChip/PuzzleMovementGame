using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Level.Completables
{
    public class CompletableCollection : Completable
    {
        [SerializeReference] protected List<Completable> items;

        public UnityEvent OnResetList;
        public UnityEvent OnCorrectCheck;
        public UnityEvent OnIncompleteCheck;

        protected void OnEnable()
        {
            print("Hello");
            foreach (var completable in items)
            {
                completable.OnDone += CheckCompletion;
            }
        }

        protected void OnDisable()
        {
            foreach (var completable in items)
            {
                completable.OnDone -= CheckCompletion;
            }
        }

        public override void ResetState()
        {
            IsDone = false;
            print("Reset collection state");
            foreach (var completable in items)
            {
                completable.ResetState();
            }
            OnResetList.Invoke();
        }

        protected virtual void CheckCompletion()
        {
            print("Collection Check");
            IsDone = true;

            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].IsDone) continue;

                print("Collection false");
                IsDone = false;
                OnIncompleteCheck.Invoke();
                return;
            }

            print("Collection true");
            OnCorrectCheck.Invoke();
        }
    }
}