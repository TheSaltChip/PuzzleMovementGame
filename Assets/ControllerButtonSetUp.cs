using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Tutorial;
using Unity.XR.PXR.Editor;
using UnityEngine;
using UnityEngine.Serialization;

public class ControllerButtonSetUp : MonoBehaviour
{
    [SerializeField] private TutorialData tutorialData;
    [SerializeField] private GameObject trigger;
    [SerializeField] private GameObject grip;
    [SerializeField] private GameObject a;
    [SerializeField] private GameObject b;
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject menu;
    [SerializeField] private MaterialContainer materialContainer;
    [SerializeField] private ControllerLookAtHandler lookAt;
    [SerializeField] private Animation animator;

    private Material _ogTrigger;
    private Material _ogGrip;
    private Material _ogA;
    private Material _ogB;
    private Material _ogJoystick;
    private Material _ogMenu;

    private int _i;

    private void Awake()
    {
        _ogA = a.GetComponent<MeshRenderer>().material;
        _ogB = b.GetComponent<MeshRenderer>().material;
        _ogTrigger = trigger.GetComponent<MeshRenderer>().material;
        _ogGrip = grip.GetComponent<MeshRenderer>().material;
        _ogJoystick = joystick.GetComponent<MeshRenderer>().material;
        _ogMenu = menu.GetComponent<MeshRenderer>().material;
    }

    public void ActivateAnimationAndGlow()
    {
        a.GetComponent<MeshRenderer>().material = _ogA;
        b.GetComponent<MeshRenderer>().material = _ogB;
        trigger.GetComponent<MeshRenderer>().material = _ogTrigger;
        grip.GetComponent<MeshRenderer>().material = _ogGrip;
        joystick.GetComponent<MeshRenderer>().material = _ogJoystick;
        menu.GetComponent<MeshRenderer>().material = _ogMenu;
        switch (tutorialData.button)
        {
            case VRControllerButtons.Trigger:
                trigger.GetComponent<MeshRenderer>().material = materialContainer.material;
                print("Correct");
                animator.Play("CubeAction");
                lookAt.SideView();
                break;
            case VRControllerButtons.Grip:
                grip.GetComponent<MeshRenderer>().material = materialContainer.material;
                animator.Play("SideButtonAction");
                lookAt.SideView();
                break;
            case VRControllerButtons.A:
                a.GetComponent<MeshRenderer>().material = materialContainer.material;
                lookAt.TopView();
                break;
            case VRControllerButtons.B:
                b.GetComponent<MeshRenderer>().material = materialContainer.material;
                lookAt.TopView();
                break;
            case VRControllerButtons.Joystick:
                joystick.GetComponent<MeshRenderer>().material = materialContainer.material;
                lookAt.TopView();
                if (tutorialData.right)
                {
                    if (_i == 0)
                    {
                        animator.Play("JoystickAction");
                        _i++;
                    }
                    else
                    {
                        animator.Play("Action");
                        _i--;
                    }
                    
                }else if (tutorialData.left)
                {
                    animator.Play(("JoystickAction.001"));
                }
                lookAt.TopView();
                break;
            case VRControllerButtons.Menu:
                menu.GetComponent<MeshRenderer>().material = materialContainer.material;
                lookAt.TopView();
                animator.Play("MenuAction");
                break;
            default:
                print("How did you get here?");
                break;
        }
    }
}
