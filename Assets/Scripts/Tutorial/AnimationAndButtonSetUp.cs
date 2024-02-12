using System;
using Tutorial;
using Unity.XR.PXR;
using UnityEngine;

public class AnimationAndButtonSetUp : MonoBehaviour
{
    [SerializeField] private GameObject ControllerL;
    [SerializeField] private GameObject ControllerR;
    [SerializeField] private TutorialData tutorialData;

    private void Start()
    {
        try
        {
                
            ControllerL.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
            ControllerR.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
        }
        catch (Exception e)
        {
                
            ControllerL.GetComponent<ControllerButton>().ActivateAnimationAndGlow();
            ControllerR.GetComponent<ControllerButton>().ActivateAnimationAndGlow();
        }
    }

    public void Activate()
    {
        if (tutorialData.selectedHand == SelectedHand.Both)
        {
            try
            {
                
                ControllerL.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
                ControllerR.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
            }
            catch (Exception e)
            {
                
                ControllerL.GetComponent<ControllerButton>().ActivateAnimationAndGlow();
                ControllerR.GetComponent<ControllerButton>().ActivateAnimationAndGlow();
            }
        }else if (tutorialData.selectedHand == SelectedHand.Right)
        {
            try
            {
                ControllerR.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
            }
            catch (Exception e)
            {
                ControllerR.GetComponent<ControllerButton>().ActivateAnimationAndGlow();
            }
        }
        else if (tutorialData.selectedHand == SelectedHand.Left)
        {
            try
            {
                ControllerL.GetComponent<ControllerButtonSetUp>().ActivateAnimationAndGlow();
            }
            catch (Exception e)
            {
                ControllerL.GetComponent<ControllerButton>().ActivateAnimationAndGlow();
            }
        }
    }

    public void End()
    {
        ControllerL.SetActive(false);
        ControllerR.SetActive(false);
    }
}
