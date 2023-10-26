using System;
using Autohand;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(PhysicsGadgetButton), typeof(AudioSource))]
    public class ButtonAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private PhysicsGadgetButton button;

        private void Awake()
        {
            button.OnPressed.AddListener(PlayRandomClickSound);
        }

        private void PlayRandomClickSound()
        {
            source.clip = ColorRecognitionBoard.Instance.GetRandomClickSound();
            source.Play();
            print("Sound");
        }
    }
}