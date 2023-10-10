using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public abstract class Completable : MonoBehaviour
    {
        public abstract bool IsDone();
        public abstract void ResetState();

        public override string ToString()
        {
            return $"IsDone: {IsDone()}";
        }
    }

    [Serializable]
    public class Tasks
    {
        [SerializeField] private List<Completable> items;
        [SerializeField] private bool sequenceMatters;

        public bool IsDone { get; private set; }
        private int _completedIndex;

        public Tasks()
        {
        }

        public Tasks(List<Completable> items, bool sequenceMatters)
        {
            this.items = items;
            this.sequenceMatters = sequenceMatters;
        }

        public bool CheckCompletion()
        {
            switch (sequenceMatters)
            {
                case true:
                    CheckNonSequencedCompletion();
                    break;
                default:
                    CheckNonSequencedCompletion();
                    break;
            }

            return IsDone;
        }

        private void CheckNonSequencedCompletion()
        {
            IsDone = true;

            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].IsDone()) continue;

                IsDone = false;
                break;
            }
        }

        private void CheckSequencedCompletion()
        {
            IsDone = true;

            for (var i = _completedIndex; i < items.Count; i++)
            {
                if (items[i].IsDone())
                {
                    _completedIndex = i;
                    continue;
                }

                IsDone = false;

                CheckRestOfList(i);

                break;
            }
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

        private void ResetList()
        {
            _completedIndex = 0;
            foreach (var item in items)
            {
                item.ResetState();
            }
        }
    }

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Tasks tasks;


        private void Update()
        {
            if (tasks.IsDone)
            {
            }
        }
    }
}