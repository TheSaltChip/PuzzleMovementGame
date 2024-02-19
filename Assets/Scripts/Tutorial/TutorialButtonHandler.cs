using System;
using UnityEngine;

namespace Tutorial
{
    public class TutorialButtonHandler : MonoBehaviour
    {
        [SerializeField] private GameObject prev;
        [SerializeField] private GameObject next;
        [SerializeField] private GameObject start;
        [SerializeField] private GameObject end;

        public void First()
        {
            next.SetActive(false);
            prev.SetActive(false);
            start.SetActive(true);
        }

        public void Last()
        {
            next.SetActive(false);
            prev.SetActive(true);
            end.SetActive(true);
        }

        public void StartTut()
        {
            next.SetActive(true);
            prev.SetActive(true);
            start.SetActive(false);
        }

        public void End()
        {
            prev.SetActive(false);
            end.SetActive(false);
        }

        public void Middle()
        {
            if(!prev.activeSelf)
                prev.SetActive(true);
            if(!next.activeSelf)
                next.SetActive(true);
            if(start.activeSelf)
                start.SetActive(false);
            if(end.activeSelf)
                end.SetActive(false);
        }
    }
}
