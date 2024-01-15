using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Variables;

public class intToSO : MonoBehaviour
{
    [SerializeField] private IntVariable var;
    public UnityEvent ev;

    public void Increment()
    {
        var.value++;
        ev.Invoke();
    }

    public void Decrement()
    {
        var.value--;
    }
}
