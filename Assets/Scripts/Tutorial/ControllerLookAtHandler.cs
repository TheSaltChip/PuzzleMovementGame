using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLookAtHandler : MonoBehaviour
{
    [SerializeField] private Transform controllerL;
    [SerializeField] private Transform controllerR;
    [SerializeField] private Transform mockControllerRotation;
    private Quaternion _mockRotation;

    private Quaternion _originalRotationL;
    private Quaternion _originalRotationR;
    
    void Start()
    {
        _mockRotation = mockControllerRotation.rotation;
        _originalRotationL = controllerL.rotation;
        _originalRotationR = controllerR.rotation;
    }

    private void SideView()
    {
        controllerL.rotation = _originalRotationL;
        controllerR.rotation = _originalRotationR;
    }

    private void TopView()
    {
        controllerL.rotation = _mockRotation;
        controllerR.rotation = _mockRotation;
    }

    public void ChangeView(bool view)
    {
        if (!view)
        {
            SideView();
        }
        else
        {
            TopView();
        }
    }

}
