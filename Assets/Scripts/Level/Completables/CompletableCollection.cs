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

        protected void OnEnable()
        {
            foreach (var completable in items)
            {
                completable.OnDone.AddListener(CheckCompletion);
            }
        }

        protected void OnDisable()
        {
            foreach (var completable in items)
            {
                completable.OnDone.RemoveListener(CheckCompletion);
            }
        }

        public override void ResetState()
        {
            IsDone = false;
            foreach (var completable in items)
            {
                completable.ResetState();
            }
            OnResetState.Invoke();
        }

        protected virtual void CheckCompletion()
        {
            foreach (var completable in items)
            {
                if (completable.IsDone) continue;
                IsDone = false;
                OnIncompleteCheck.Invoke();
                return;
            }
            
            Completed();
        }
    }
}