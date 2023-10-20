using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class TutorialText : MonoBehaviour
    {
        [SerializeField] private string view;
        [SerializeField] private TMP_Text text;
        [SerializeField] private bool first;
        [SerializeField] private bool last;
        [SerializeField] private GameObject[] mediums;
        private Material _originalMaterial;
        private Material _highlight;

        private void Start()
        {
            _originalMaterial = mediums[0].GetComponent<Renderer>().material;
            _highlight = HighlightStore.GetMaterial();
        }

        public string GetView()
        {
            return view;
        }

        public string GetText()
        {
            return text.text;
        }

        public bool IsLast()
        {
            return last;
        }

        public bool IsFirst()
        {
            return first;
        }

        public void ChangeActive(bool active)
        {
            gameObject.SetActive(active);
            foreach (var medium in mediums)
            {
                medium.GetComponent<Renderer>().material = active ? _highlight : _originalMaterial;
            }
        }
    }
}