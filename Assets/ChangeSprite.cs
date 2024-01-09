using System.Collections;
using System.Collections.Generic;
using Puzzle.Scriptables;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    [SerializeField]
    private GoalSprite goalSprite;
    
    public void ChangeImage()
    { 
        var rect = GetComponent<RectTransform>().sizeDelta;
        var sprite = goalSprite.image;
        var width = rect.x;
        var height = rect.y;
        var swidth = sprite.rect.width;
        var sheight = sprite.rect.height;
        print(width);
        print(height);
        if (swidth < sheight)
        {
            var p = swidth / sheight;
            //GetComponent< RectTransform >().SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, height);
            print(p);
            print(sheight);
            print(swidth);
            width *= p;
            GetComponent< RectTransform >().SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, width);
        }else if ((int)swidth == (int)sheight)
        {
            GetComponent< RectTransform >().SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, width-height*1.5f);
        }
        gameObject.GetComponent<Image>().sprite = sprite;
    }
}
