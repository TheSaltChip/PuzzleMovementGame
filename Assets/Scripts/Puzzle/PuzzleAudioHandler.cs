using UnityEngine;
using Variables;

namespace Puzzle
{
    public class PuzzleAudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip correct;
        [SerializeField] private AudioClip incorrect;
        [SerializeField] private BoolVariable state;

        public void PlaySound()
        {
            source.PlayOneShot(state.value ? correct : incorrect);
        }
    }
}
