#region License
// Copyright (C) 2024 Sebastian Misje Jonassen & Mathias Nupen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Commons Clause License version 1.0 with GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Commons Clause License and GNU General Public License for more details.
// 
// You should have received a copy of the Commons Clause License and GNU General Public License
// along with this program.  If not, see <https://commonsclause.com/> and <https://www.gnu.org/licenses/>.
#endregion

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

        private Quaternion ogRotation;
        private Transform tr;
        private bool started;

        private VRControllerButtons prev;
        private MeshRenderer[] buttons;
        private Material[] materials;

        private bool first = true;
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

        private void OnEnable()
        {
            SetUpMaterials();
            ActivateAnimationAndGlow();
        }

        public void ResetAnimationAndMaterial()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            var mat = materials[(int)prev];
            buttons[(int)prev].material = mat;
            animator.Stop();
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

            if (useLookAt)
            {
                tr.rotation = tutorialData.button is VRControllerButtons.Trigger or VRControllerButtons.Grip ? ogRotation : Quaternion.Euler(lookAt);
            }
            
            animator.Stop();
            
            switch (tutorialData.button)
            {
                case VRControllerButtons.Trigger:
                    trigger.material = materialContainer.material;
                    if (!tutorialData.skipAnimation)
                    {
                        animator.Play("CubeAction");
                    }
                    break;
                case VRControllerButtons.Grip:
                    grip.material = materialContainer.material;
                    if (!tutorialData.skipAnimation)
                    {
                        animator.Play("SideButtonAction");
                    }
                    break;
                case VRControllerButtons.A:
                    a.material = materialContainer.material;
                    if (!tutorialData.skipAnimation)
                    {
                        animator.Play("AAction");
                    }
                    break;
                case VRControllerButtons.B:
                    b.material = materialContainer.material;
                    if (!tutorialData.skipAnimation)
                    {
                        animator.Play("BAction");
                    }
                    break;
                case VRControllerButtons.Joystick:
                    joystick.material = materialContainer.material;
                    switch (tutorialData.selectedHand)
                    {
                        case SelectedHand.Right when _i == 0:
                            if (!tutorialData.skipAnimation)
                            {
                                animator.Play("JoystickAction");
                            }
                            _i++;
                            break;
                        case SelectedHand.Right:
                            if (!tutorialData.skipAnimation)
                            {
                                animator.Play("Action");
                            }
                            _i--;
                            break;
                        case SelectedHand.Left:
                            if (!tutorialData.skipAnimation)
                            {
                                animator.Play(("JoystickAction.001"));
                            }
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
                    if (!tutorialData.skipAnimation)
                    {
                        animator.Play("MenuAction");
                    }
                    break;
                default:
                    print("How did you get here?");
                    break;
            }
            prev = tutorialData.button;
        }
    }
}
