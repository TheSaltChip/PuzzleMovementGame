using Events;
using UnityEngine;

namespace Tutorial
{
    public class ControllerButtonSetUp : MonoBehaviour
    {
        [SerializeField] private TutorialData tutorialData;
        [SerializeField] private MeshRenderer trigger;
        [SerializeField] private MeshRenderer grip;
        [SerializeField] private MeshRenderer a;
        [SerializeField] private MeshRenderer b;
        [SerializeField] private MeshRenderer joystick;
        [SerializeField] private MeshRenderer menu;
        [SerializeField] private MaterialContainer materialContainer;
        [SerializeField] private Vector3 lookAt;
        [SerializeField] private bool useLookAt;
        [SerializeField] private Animation animator;
        [SerializeField] private SelectedHand side;

        [SerializeField] private GameEvent ready;

        private Quaternion ogRotation;
        private Transform tr;
        private bool started;

        private VRControllerButtons prev;
        private MeshRenderer[] buttons;
        private Material[] materials;

        private int _i;


        private void  SetUpMaterials()
        {
            tr = gameObject.transform;
            ogRotation = tr.rotation;
            buttons = new[] {trigger,grip,a,b,joystick,menu};
            materials = new[] { trigger.material, grip.material, a.material, b.material, joystick.material, menu.material };
            prev = VRControllerButtons.Trigger;
            started = true;
        }

        public void ActivateAnimationAndGlow()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }

            if (!started)
            {
                SetUpMaterials();
            }
            var mat = materials[(int)prev];
            buttons[(int)prev].material = mat;
            animator.Stop();

            if (side != tutorialData.selectedHand)
            {
                if (tutorialData.selectedHand != SelectedHand.Both)
                {
                    return;
                }
            }

            if (useLookAt)
            {
                tr.rotation = tutorialData.button is VRControllerButtons.Trigger or VRControllerButtons.Grip ? ogRotation : Quaternion.Euler(lookAt);
            }
            
            switch (tutorialData.button)
            {
                case VRControllerButtons.Trigger:
                    trigger.material = materialContainer.material;
                    animator.Play("CubeAction");
                    break;
                case VRControllerButtons.Grip:
                    if (!grip.gameObject.activeSelf)
                    {
                        ready.Raise();
                    }
                    grip.material = materialContainer.material;
                    animator.Play("SideButtonAction");
                    break;
                case VRControllerButtons.A:
                    a.material = materialContainer.material;
                    break;
                case VRControllerButtons.B:
                    b.material = materialContainer.material;
                    break;
                case VRControllerButtons.Joystick:
                    joystick.material = materialContainer.material;
                    switch (tutorialData.selectedHand)
                    {
                        case SelectedHand.Right when _i == 0:
                            animator.Play("JoystickAction");
                            _i++;
                            break;
                        case SelectedHand.Right:
                            animator.Play("Action");
                            _i--;
                            break;
                        case SelectedHand.Left:
                            animator.Play(("JoystickAction.001"));
                            break;
                        case SelectedHand.Both:
                            break;
                        default:
                            print("You should not be here");
                            break;
                    }
                    break;
                case VRControllerButtons.Menu:
                    menu.material = materialContainer.material;
                    animator.Play("MenuAction");
                    break;
                default:
                    print("How did you get here?");
                    break;
            }
            prev = tutorialData.button;
        }
    }
}
