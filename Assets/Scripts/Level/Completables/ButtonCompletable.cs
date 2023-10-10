using Autohand;
using UnityEngine;

namespace Level.Completables
{
    [RequireComponent(typeof(PhysicsGadgetButton))]
    public class ButtonCompletable : Completable
    {
        [SerializeField] private PhysicsGadgetButton button;

        private bool _pressed;

        private void Awake()
        {
            button.OnPressed.AddListener(Pressed);
        }

        private void Pressed()
        {
            _pressed = true;
        }

        public override bool IsDone()
        {
            return _pressed;
        }

        public override void ResetState()
        {
            _pressed = false;
        }
    }
}