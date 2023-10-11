using UnityEngine;

namespace Level.Completables
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
}