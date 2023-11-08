using System.Globalization;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ChangeText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public void SetFloat(float inputText)
        {
            text.text = inputText
                .ToString(CultureInfo.InvariantCulture);
        }
    }
}