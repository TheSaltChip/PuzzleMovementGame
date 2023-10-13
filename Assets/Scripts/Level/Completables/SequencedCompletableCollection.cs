using UnityEngine.Events;

namespace Level.Completables
{
    /// <summary>
    /// A collection of Completable objects that need to be completed in a specific sequence.
    /// </summary>
    public class SequencedCompletableCollection : CompletableCollection
    {
        private int _completedIndex;

        /// <summary>
        /// Resets the state of the collection and the completed index.
        /// </summary>
        public override void ResetState()
        {
            base.ResetState();
            _completedIndex = 0;
        }

        /// <summary>
        /// Checks the completion state of the collection by iterating through the items.
        /// </summary>
        protected override void CheckCompletion()
        {
            IsDone = true;

            for (var i = _completedIndex; i < items.Count; i++)
            {
                if (items[i].IsDone)
                {
                    _completedIndex = i;
                    continue;
                }

                IsDone = false;

                CheckRestOfList(i);

                return;
            }

            OnDone.Invoke();
        }

        /// <summary>
        /// Checks the completion state of the remaining items in the collection starting from a specified index.
        /// </summary>
        /// <param name="start">The index from which to start checking.</param>
        private void CheckRestOfList(int start)
        {
            for (var j = start; j < items.Count; j++)
            {
                if (!items[j].IsDone) continue;

                OnFailedCheck.Invoke();
                ResetState();
                return;
            }

            OnIncompleteCheck.Invoke();
        }
    }

}