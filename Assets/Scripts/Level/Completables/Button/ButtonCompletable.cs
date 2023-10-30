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
            button.OnPressed.AddListener(Completed);
        }

        private void OnDisable()
        {
            button.OnPressed.RemoveListener(Completed);
        }
    }
}