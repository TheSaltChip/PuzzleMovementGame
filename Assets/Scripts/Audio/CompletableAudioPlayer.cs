using Completables;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(Completable), typeof(AudioSource))]
    public class CompletableAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private Completable completable;
        [SerializeField] private AudioClip doneClip;
        [SerializeField] private AudioClip failedClip;

        private void Start()
        {
            completable.OnDone.AddListener(PlayOnDoneSound);
            completable.OnFailedCheck.AddListener(PlayOnFailedSound);
        }

        private void PlayOnDoneSound()
        {
            source.clip = doneClip;
            source.Play();
        }
        
        private void PlayOnFailedSound()
        {
            source.clip = failedClip;
            source.Play();
        }
    }
}