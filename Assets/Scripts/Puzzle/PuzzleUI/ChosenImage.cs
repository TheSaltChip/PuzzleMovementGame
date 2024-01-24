using System.Collections;
using System.Collections.Generic;
using Puzzle.Scriptables;
using UnityEngine;
using UnityEngine.Events;

public class ChosenImage : MonoBehaviour
{
    [SerializeField] private Texture2D image;
    [SerializeField] private SelectedImage store;
    public UnityEvent completed;

    public void ChangeImage()
    {
        store.currentSelected = image;
        completed.Invoke();
    }
}
