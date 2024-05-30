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

using UnityEngine;
using UnityEngine.Events;

namespace Completables
{
    /// <summary>
    ///     An abstract base class for objects that represent something which can be completed.
    /// </summary>
    public abstract class Completable : MonoBehaviour
    {
        public UnityEvent OnDone;
        public UnityEvent OnIncompleteCheck;
        public UnityEvent OnFailedCheck;
        public UnityEvent OnResetState;

        /// <summary>
        ///     Gets a value indicating whether the object is in a 'done' state.
        /// </summary>
        public bool IsDone { get; private set; }

        /// <summary>
        ///     Resets the state of the completable object.
        /// </summary>
        public virtual void ResetState()
        {
            IsDone = false;
            OnResetState?.Invoke();
        }

        protected void Completed()
        {
            IsDone = true;
            OnDone.Invoke();
        }

        /// <summary>
        ///     Returns a string representation of the Completable object.
        /// </summary>
        /// <returns>A string containing the 'IsDone' state.</returns>
        public override string ToString()
        {
            return $"IsDone: {IsDone}";
        }
    }
}