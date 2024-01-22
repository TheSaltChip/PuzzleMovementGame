using Completables;
using UnityEngine;

namespace Audio
{
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
            source.PlayOneShot(doneClip);
        }
        
        private void PlayOnFailedSound()
        {
            source.PlayOneShot(failedClip);
        }
    }
}