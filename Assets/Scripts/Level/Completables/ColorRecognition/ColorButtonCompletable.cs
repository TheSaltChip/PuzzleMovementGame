using System;
using System.Collections;
using Autohand;
using Level.Completables.Button;
using UnityEngine;

namespace Level.Completables.ColorRecognition
{
    [RequireComponent(typeof(PhysicsGadgetButton), typeof(MeshRenderer))]
    public class ColorButtonCompletable : Completable
    {
        [SerializeField] private PhysicsGadgetButton button;
        [SerializeField] private MeshRenderer meshRenderer;

        private Color _dimmed;
        private Color _lit;

        private void Awake()
        {
            var material = meshRenderer.material;

            _lit = material.color;

            Color.RGBToHSV(material.color, out var h, out var s, out _);

            _dimmed = Color.HSVToRGB(h, s, 0.5f);

            material.color = _dimmed;
        }

        public IEnumerator Blink(float duration)
        {
            var material = meshRenderer.material;

            material.color = _lit;

            yield return new WaitForSeconds(duration);

            material.color = _dimmed;
        }

        private void TurnOn()
        {
            meshRenderer.material.color = _lit;
        }
        private void TurnOff()
        {
            meshRenderer.material.color = _dimmed;
        }

        private void OnEnable()
        {
            button.OnPressed.AddListener(Pressed);
            button.OnUnpressed.AddListener(TurnOff);
        }

        private void OnDisable()
        {
            button.OnPressed.RemoveListener(Pressed);
            button.OnUnpressed.RemoveListener(TurnOff);
        }

        private void Pressed()
        {
            TurnOn();
            IsDone = true;
            OnDone.Invoke();
        }
    }
}