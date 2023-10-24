using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject prev;
    [SerializeField] private GameObject next;
    [SerializeField] private TextHandler textHandler;
    [SerializeField] private ControllerLookAtHandler controllerLookAtHandler;

    private bool _prevActive = true;
    private bool _nextActive;
    
    void Start()
    {
        PreviousActive();
        NextActive();
    }

    public void NextText()
    {
        (bool t,bool str) = textHandler.NextText();
        if (t)
        {
            NextActive();
        }
        if(!prev.activeSelf)
            PreviousActive();
        controllerLookAtHandler.ChangeView(str);
    }

    public void PreviousText()
    {
        (bool t,bool str) = textHandler.PreviousText();
        if (t)
        {
            PreviousActive();
        }
        if(!next.activeSelf)
            NextActive();
        controllerLookAtHandler.ChangeView(str);
    }
    
    private void NextActive()
    {
        _nextActive = !_nextActive;
        next.SetActive(_nextActive);
    }

    private void PreviousActive()
    {
        _prevActive = !_prevActive;
        prev.SetActive(_prevActive);
    }
    
}
