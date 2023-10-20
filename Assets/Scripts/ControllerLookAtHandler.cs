using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerLookAtHandler : MonoBehaviour
{
    [SerializeField] private GameObject controllerL;
    [SerializeField] private GameObject controllerR;
    [SerializeField] private Transform mockControllerRotation;
    private Quaternion _mockRotation;

    private Quaternion _originalRotationL;
    private Quaternion _originalRotationR;
    
    void Start()
    {
        _mockRotation = mockControllerRotation.rotation;
    }

    private void SideView()
    {
        controllerL.transform.rotation = _originalRotationL;
        controllerR.transform.rotation = _originalRotationR;
    }

    private void TopView()
    {
        controllerL.transform.rotation = _mockRotation;
        controllerR.transform.rotation = _mockRotation;
    }

    public void ChangeView(string view)
    {
        if (view.Equals("side"))
        {
            SideView();
        }
        else
        {
            TopView();
        }
    }

}
