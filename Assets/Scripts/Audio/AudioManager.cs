using System;
using UnityEngine;

namespace Audio
{
    //Credit to Brackeys youtube tutorial on Audio managers, as the majority of this code and learning how to use it was made by him.
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0, 1)] public float volume;
        [Range(-3, 3)] public float pitch;
        public bool loop;
        public AudioSource source;

        public Sound()
        {
            volume = 1;
            pitch = 1;
            loop = false;
        }
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public Sound[] sounds;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);

            foreach (var s in sounds)
            {
                if (!s.source)
                    s.source = gameObject.AddComponent<AudioSource>();

                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        public void Play(string soundName)
        {
            var s = Array.Find(sounds, sound => sound.name == soundName);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + soundName + " not found");
                return;
            }

            s.source.Play();
        }

        public void Stop(string soundName)
        {
            var s = Array.Find(sounds, sound => sound.name == soundName);

            s.source.Stop();
        }
    }
}