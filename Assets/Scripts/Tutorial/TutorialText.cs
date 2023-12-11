using System;
using TMPro;
using Tutorial;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class TutorialText : MonoBehaviour
    {
        [SerializeField] private UnityEvent first;
        [SerializeField] private UnityEvent last;
        [SerializeField] private UnityEvent middle;
        [SerializeField] private StringTableCollection strings;

        private bool _firstInLine;
        private int _start;
        private int _end;
        private int _current;
        private LocalizeStringEvent _text;

        private void Start()
        {
            _end = strings.SharedData.Entries.Count-1;
            _start = 2; //First 2 entries in the table are next and previous
            _current = _start;
            _text = gameObject.GetComponent<LocalizeStringEvent>();
            _text.StringReference.TableEntryReference = strings.SharedData.Entries[_current].Key;
            _firstInLine = true;
        }

        private void ActiveButtons()
        {
            if (_current == _start)
            {
                first.Invoke();
                print("Invoked start");
                _firstInLine = true;
            } else if (_current == _end)
            {
                last.Invoke();
                print("Invoked end");
                _firstInLine = true;
            }
            else if (_firstInLine)
            {
                middle.Invoke();
                _firstInLine = false;
            }
        }

        public void NextText()
        {
            if (_current == _end)
                return;
            _current++;
            _text.StringReference.TableEntryReference = strings.SharedData.Entries[_current].Key;
            ActiveButtons();
        }

        public void PreviousText()
        {
            if (_current == _start)
                return;
            _current--;
            _text.StringReference.TableEntryReference = strings.SharedData.Entries[_current].Key;
            ActiveButtons();
        }
    }
}