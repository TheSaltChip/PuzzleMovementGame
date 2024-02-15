using System;
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
        [SerializeField] private ControllerLookAtHandler lookAt;
        [SerializeField] private bool useLookAt;
        [SerializeField] private Animation animator;
        [SerializeField] private SelectedHand side;

        private Material _ogTrigger;
        private Material _ogGrip;
        private Material _ogA;
        private Material _ogB;
        private Material _ogJoystick;
        private Material _ogMenu;

        private VRControllerButtons prev;
        private MeshRenderer[] buttons;
        private Material[] materials;

        private int _i;


        private void  OnEnable()
        {
            buttons = new[] {trigger,grip,a,b,joystick,menu};
            
            /*_ogA = a.material;
            _ogB = b.material;
            _ogTrigger = trigger.material;
            _ogGrip = grip.material;
            _ogJoystick = joystick.material;
            _ogMenu = menu.material;*/
            materials = new[] { trigger.material, grip.material, a.material, b.material, joystick.material, menu.material };
            prev = VRControllerButtons.Trigger;
        }

        public void ActivateAnimationAndGlow()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            var mat = materials[(int)prev];
            buttons[(int)prev].material = mat;
            animator.Stop();

            if (side != tutorialData.selectedHand)
            {
                return;
            }

            if (useLookAt)
            {
                if (tutorialData.button is VRControllerButtons.Trigger or VRControllerButtons.Grip)
                {
                    lookAt.SideView();
                }
                else
                {
                    lookAt.TopView();
                }
            }
            
            
            switch (tutorialData.button)
            {
                case VRControllerButtons.Trigger:
                    trigger.material = materialContainer.material;
                    animator.Play("CubeAction");
                    break;
                case VRControllerButtons.Grip:
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
                            throw new ArgumentOutOfRangeException();
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
