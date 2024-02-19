using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPrint : MonoBehaviour
{
    [SerializeField] private string message;

    public void Printing()
    {
        print(message);
    }
}
