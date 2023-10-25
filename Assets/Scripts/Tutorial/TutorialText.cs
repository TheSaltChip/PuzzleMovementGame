using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class TutorialText : MonoBehaviour
    {
        [SerializeField] private bool view; // false = Side view, true = Top view
        [SerializeField] private TMP_Text text;
        [SerializeField] private bool first;
        [SerializeField] private bool last;
        [SerializeField] private MeshRenderer[] mediums;
        [SerializeField] private AnimationClip[] anims;
        [SerializeField] private Animation animL;
        [SerializeField] private Animation animR;
        [SerializeField] private int hand; //0 = left, 1 = right, 2 = both
        private Material _originalMaterial;
        private Material _highlight;
        private bool _initial = true;

        private void SetUp()
        {
            if (mediums.Length <= 0) return;
            
            _originalMaterial = mediums[0].material;
            _highlight = HighlightStore.Instance.GetMaterial();
            
            
        }

        public bool GetView()
        {
            return view;
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
            if (_initial)
            {
                SetUp();
                _initial = false;
            }
            gameObject.SetActive(active);
            if (anims.Length > 0 && active)
            {
                if (hand == 0)
                {
                    animL.Play(anims[0].name);
                }else if (hand == 1)
                {
                    animR.Play(anims[0].name);
                }
                else if(anims.Length == 2 && hand == 2)
                {
                    animL.Play(anims[0].name);
                    animR.Play(anims[1].name);
                }
            }
            foreach (var medium in mediums)
            {
                medium.material = active ? _highlight : _originalMaterial;
            }
        }
    }
}