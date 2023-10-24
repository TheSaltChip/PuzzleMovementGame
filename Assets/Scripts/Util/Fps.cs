using System.Collections;
using TMPro;
using UnityEngine;

namespace Util
{
    public class Fps : MonoBehaviour
    {
        private float count;
        private WaitForSeconds _waitForSeconds;
        [SerializeField] private TMP_Text text;

        private IEnumerator Start()
        {
            _waitForSeconds = new WaitForSeconds(0.1f);
            while (true)
            {
                count = 1f / Time.unscaledDeltaTime;
                yield return _waitForSeconds;
            }
        }

        private void OnGUI()
        {
            text.text = $"FPS: {Mathf.Round(count)}";
        }
    }
}