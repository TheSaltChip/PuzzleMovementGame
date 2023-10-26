using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class ColorRecognitionBoard : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;
        
        public static ColorRecognitionBoard Instance;

        private void Awake()
        {
            Instance = this;
        }

        public AudioClip GetRandomClickSound()
        {
            return clips[Random.Range(0, clips.Length)];
        }
    }
}