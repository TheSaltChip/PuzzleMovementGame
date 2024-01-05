using System.Collections;
using System.Collections.Generic;
using Puzzle.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField]
    private GoalSprite goalSprite;
    
    public void ChangeImage()
    {
        gameObject.GetComponent<Image>().sprite = goalSprite.image;
    }
}
