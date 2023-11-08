using Completables;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(Completable), typeof(AudioSource))]
    public class CompletableRandomAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private Completable completable;
        [SerializeField] private AudioClip[] clips;

        private void Awake()
        {
            completable.OnDone.AddListener(PlayRandomClickSound);
        }

        private void PlayRandomClickSound()
        {
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
        }
    }
}