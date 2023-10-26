using Autohand;
using Level.Completables;
using UnityEngine;
using UnityEngine.Serialization;

namespace Audio
{
    [RequireComponent(typeof(Completable), typeof(AudioSource))]
    public class CompletableRandomAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private Completable button;
        [SerializeField] private AudioClip[] clips;

        private void Awake()
        {
            button.OnDone.AddListener(PlayRandomClickSound);
        }

        private void PlayRandomClickSound()
        {
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
        }
    }
}