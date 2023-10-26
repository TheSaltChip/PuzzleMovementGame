using Autohand;
using UnityEngine;

namespace Level.Completables.Button
{
    /// <summary>
    /// A Completable that represents the completion state of a button press.
    /// </summary>
    [RequireComponent(typeof(PhysicsGadgetButton))]
    public class ButtonCompletable : Completable
    {
        [SerializeField] private PhysicsGadgetButton button;
        
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