using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class TextHandler : MonoBehaviour
    {
        [SerializeField] private TutorialText[] texts;
        private int _i;

        private void Start()
        {
            texts[_i].ChangeActive(true);
        }

        public (bool,string) NextText()
        {
            texts[_i].ChangeActive(false);
            _i++;
            var temp = texts[_i];
            temp.ChangeActive(true);
            return (temp.IsLast(),temp.GetView());
        }
                
        public (bool,string) PreviousText()
        {
            texts[_i].ChangeActive(false);
            _i--;
            var temp = texts[_i];
            temp.ChangeActive(true);
            return (temp.IsFirst(),temp.GetView());
        }
    }
}