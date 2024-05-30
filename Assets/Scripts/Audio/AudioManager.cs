#region License
// Copyright (C) 2024 Sebastian Misje Jonassen & Mathias Nupen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Commons Clause License version 1.0 with GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Commons Clause License and GNU General Public License for more details.
// 
// You should have received a copy of the Commons Clause License and GNU General Public License
// along with this program.  If not, see <https://commonsclause.com/> and <https://www.gnu.org/licenses/>.
#endregion

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