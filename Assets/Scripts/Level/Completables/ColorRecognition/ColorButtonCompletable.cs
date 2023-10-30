using System.Collections;
using Autohand;
using UnityEngine;

namespace Level.Completables.ColorRecognition
{
    [RequireComponent(typeof(PhysicsGadgetButton), typeof(MeshRenderer))]
    public class ColorButtonCompletable : Completable
    {
        [SerializeField] private PhysicsGadgetButton button;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Color correct;
        [SerializeField] private Color incorrect;

        private Material _material;
        private Color _dimmed;
        private Color _lit;

        private void Awake()
        {
            _material = meshRenderer.material;
            
            _lit = _material.color;

            Color.RGBToHSV(_material.color, out var h, out var s, out _);

            _dimmed = Color.HSVToRGB(h, s, 0.5f);

            _material.color = _dimmed;
        }

        public IEnumerator Blink(float duration)
        {
            yield return StartCoroutine(BlinkColor(duration, _lit, _dimmed));
        }

        public IEnumerator BlinkCorrectColor(float duration)
        {
            yield return StartCoroutine(BlinkColor(duration, correct, _dimmed));
        }

        public IEnumerator BlinkIncorrectColor(float duration)
        {
            yield return StartCoroutine(BlinkColor(duration, incorrect, _dimmed));
        }

        private IEnumerator BlinkColor(float duration, Color start, Color end)
        {
            _material.color = start;

            yield return new WaitForSeconds(duration);

            _material.color = end;
        }

        private void Pressed()
        {
            TurnOn();
            Completed();
        }

        private void TurnOn()
        {
            _material.color = _lit;
        }

        private void TurnOff()
        {
            _material.color = _dimmed;
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
    }
}