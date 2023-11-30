using System.Collections;
using System.Collections.Generic;
using Painting;
using UnityEngine;

public class PaintballColorManager : MonoBehaviour
{
    [SerializeField] private GameObject[] placeholderButtons;
    private GameObject _activeColor;

    public void SetColor(Color c)
    {
        for (int i = 0; i < placeholderButtons.Length; i++)
        {
            if (placeholderButtons[i].GetComponent<ColorKeeper>().GetColor().Equals(c))
            {
                placeholderButtons[i].SetActive(true);
            }
        }
    }
}
