using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using Variables;

namespace Tutorial
{
    public class TutorialText : MonoBehaviour
    {
        [SerializeField] private GameObject[] txtElements;
        [SerializeField] private UnityEvent first;
        [SerializeField] private UnityEvent last;
        [SerializeField] private UnityEvent middle;

        private StringTable _strings;

        private bool _firstInLine;
        private int _start;
        private int _end;
        private int _current;
        private LocalizeStringEvent _text;
        private int _sphereText;

        private void Awake()
        {
            _strings = LocalizationSettings.StringDatabase.GetTable("Tutorial");
            _end = txtElements.Length-2;//Not counting ball grabbed
            _current = _start;
            _sphereText = 7;//Change if BallGrabbedText is moved in the list
            _firstInLine = true;
        }

        private void ActiveButtons()
        {
            if (_current == _start)
            {
                first?.Invoke();
                _firstInLine = true;
            }
            else if (_current == _end)
            {
                last?.Invoke();
                _firstInLine = true;
            }
            else if (_firstInLine)
            {
                middle?.Invoke();
                _firstInLine = false;
            }
        }

        public void DeactivateCurrent()
        {
            txtElements[_current].SetActive(false);
        }

        public void SphereGrabbed()
        {
            DeactivateCurrent();
            txtElements[_sphereText].SetActive(true);
        }

        public void NextText()
        {
            if (_current > _end)
                return;
            DeactivateCurrent();
            _current++;
            txtElements[_current].SetActive(true);
            ActiveButtons();
        }

        public void PreviousText()
        {
            if (_current <= _start)
                return;
            DeactivateCurrent();
            _current--;
            txtElements[_current].SetActive(true);
            ActiveButtons();
        }
    }
}