using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Level.Completables
{
    /// <summary>
    /// An abstract base class for objects that represent something which can be completed.
    /// </summary>
    public abstract class Completable : MonoBehaviour
    {
        public UnityEvent OnDone;
        public UnityEvent OnIncompleteCheck;
        public UnityEvent OnFailedCheck;
        public UnityEvent OnResetState;

        /// <summary>
        /// Gets a value indicating whether the object is in a 'done' state.
        /// </summary>
        public bool IsDone { get; protected set; }

        /// <summary>
        /// Resets the state of the completable object. Derived classes must implement this method.
        /// </summary>
        public virtual void ResetState()
        {
            IsDone = false;
        }

        protected void Completed()
        {
            IsDone = true;
            OnDone.Invoke();
        }

        /// <summary>
        /// Returns a string representation of the Completable object.
        /// </summary>
        /// <returns>A string containing the 'IsDone' state.</returns>
        public override string ToString()
        {
            return $"IsDone: {IsDone}";
        }
    }
}