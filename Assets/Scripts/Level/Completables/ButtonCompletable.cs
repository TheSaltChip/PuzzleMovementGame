using Autohand;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Level.Completables
{
    /// <summary>
    /// A Completable that represents the completion state of a button press.
    /// </summary>
    public class ButtonCompletable : Completable
    {
        [SerializeField] private PhysicsGadgetButton button;

        /// <summary>
        /// Resets the state of the button completable, marking it as not done.
        /// </summary>
        public override void ResetState()
        {
            IsDone = false;
        }

        private void OnEnable()
        {
            button.OnPressed.AddListener(Pressed);
        }

        private void OnDisable()
        {
            button.OnPressed.RemoveListener(Pressed);
        }

        private void Pressed()
        {
            IsDone = true;
            OnDone.Invoke();
        }
    }
}