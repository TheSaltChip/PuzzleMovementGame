using System;
using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

public class AnimationAndButtonSetUp : MonoBehaviour
{
    [SerializeField] private GameObject ControllerL;
    [SerializeField] private GameObject ControllerR;
    [SerializeField] private TutorialData tutorialData;

    private void Start()
    {
        ControllerL.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
        ControllerR.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
    }

    public void Activate()
    {
        if (tutorialData.both)
        {
            ControllerL.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
            ControllerR.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
        }else if (tutorialData.right)
        {
            ControllerR.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
        }
        else if (tutorialData.left)
        {
            ControllerL.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
        }
    }
}
