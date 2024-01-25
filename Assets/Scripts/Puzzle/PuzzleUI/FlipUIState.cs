using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver;
using Variables;

public class FlipUIState : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    public void ChangeState()
    { 
        obj.SetActive(!obj.activeSelf);
    }
}
