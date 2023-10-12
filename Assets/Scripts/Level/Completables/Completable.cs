using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Level.Completables
{
    public abstract class Completable : MonoBehaviour
    {
        public event Action OnDone;

        public bool IsDone { get; protected set; }

        public abstract void ResetState();

        protected virtual void InvokeOnDone()
        {
            OnDone?.Invoke();
        }

        public override string ToString()
        {
            return $"IsDone: {IsDone}";
        }
    }
}