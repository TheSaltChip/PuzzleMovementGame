using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Level.Completables
{
    public class SequencedCompletableCollection : CompletableCollection
    {
        public UnityEvent OnFailedCheck;
        
        private int _completedIndex;

        public override void ResetState()
        {
            base.ResetState();
            print("Reset sequenced collection state");
            _completedIndex = 0;
        }

        protected override void CheckCompletion()
        {
            print("Sequenced Collection Check");

            IsDone = true;

            for (var i = _completedIndex; i < items.Count; i++)
            {
                if (items[i].IsDone)
                {
                    _completedIndex = i;
                    continue;
                }

                IsDone = false;
                print("Sequenced Collection false");

                CheckRestOfList(i);

                return;
            }
            
            OnCorrectCheck.Invoke();
            print("Sequenced Collection true");
        }

        private void CheckRestOfList(int start)
        {
            for (var j = start; j < items.Count; j++)
            {
                if (!items[j].IsDone) continue;

                print("Sequenced Collection reset");
                OnFailedCheck.Invoke();
                ResetState();
                return;
            }
            
            OnIncompleteCheck.Invoke();
        }
    }
}