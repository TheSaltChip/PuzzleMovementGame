using Tutorial;
using UnityEngine;

public class AnimationAndButtonSetUp : MonoBehaviour
{
    [SerializeField] private GameObject ControllerL;
    [SerializeField] private GameObject ControllerR;
    [SerializeField] private TutorialData tutorialData;

    private ControllerButtonSetUp l;
    private ControllerButtonSetUp r;

    private void Start()
    {
        l = ControllerL.GetComponent<ControllerButtonSetUp>();
        l.ActivateAnimationAndGlow();
        r = ControllerR.GetComponent<ControllerButtonSetUp>();
        r.ActivateAnimationAndGlow();
    }

    public void Activate()
    {
        l.ResetAnimationAndMaterial();
        r.ResetAnimationAndMaterial();
        
        switch (tutorialData.selectedHand)
        {
            case SelectedHand.Left:
                l.ActivateAnimationAndGlow();
                break;
            case SelectedHand.Right:
                r.ActivateAnimationAndGlow();
                break;
            case SelectedHand.Both:
            default:
                l.ActivateAnimationAndGlow();
                r.ActivateAnimationAndGlow();
                break;
        }
    }

    public void End()
    {
        ControllerL.SetActive(false);
        ControllerR.SetActive(false);
    }
}
