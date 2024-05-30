#region License
// Copyright (C) 2024 Sebastian Misje Jonassen & Mathias Nupen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Commons Clause License version 1.0 with GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Commons Clause License and GNU General Public License for more details.
// 
// You should have received a copy of the Commons Clause License and GNU General Public License
// along with this program.  If not, see <https://commonsclause.com/> and <https://www.gnu.org/licenses/>.
#endregion

namespace Completables
{
    /// <summary>
    ///     A collection of Completable objects that need to be completed in a specific sequence.
    /// </summary>
    public class SequencedCompletableCollection : CompletableCollection
    {
        private int _completedIndex;

        /// <summary>
        ///     Resets the state of the collection and the completed index.
        /// </summary>
        public override void ResetState()
        {
            base.ResetState();
            _completedIndex = 0;
        }

        /// <summary>
        ///     Checks the completion state of the collection by iterating through the items.
        /// </summary>
        protected override void CheckCompletion()
        {
            for (var i = _completedIndex; i < items.Count; i++)
            {
                if (items[i].IsDone)
                {
                    _completedIndex = i;
                    continue;
                }

                CheckRestOfList(i);

                return;
            }

            Completed();
        }

        /// <summary>
        ///     Checks the completion state of the remaining items in the collection starting from a specified index.
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