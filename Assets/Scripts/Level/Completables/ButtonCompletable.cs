using Autohand;
using UnityEngine;
using UnityEngine.Events;

namespace Level.Completables
{
    public class ButtonCompletable : Completable
    {
        [SerializeField] private PhysicsGadgetButton button;

        public UnityEvent OnPressed;

        private void Awake()
        {
            print("Button");
            button.OnPressed.AddListener(Pressed);
            
            button.OnPressed.AddListener(InvokeOnPressed);
        }

        private void InvokeOnPressed()
        {
            OnPressed.Invoke();
        }

        private void OnDisable()
        {
            button.OnPressed.RemoveListener(Pressed);
        }

        private void Pressed()
        {
            print("BUTTON PRESSED");
            IsDone = true;
            InvokeOnDone();
        }

        public override void ResetState()
        {
            print("Reset button state");
            IsDone = false;
        }
    }
}