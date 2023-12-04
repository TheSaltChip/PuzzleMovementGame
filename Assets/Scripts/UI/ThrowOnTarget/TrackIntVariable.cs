using TMPro;
using UnityEngine;
using Variables;

namespace UI.ThrowOnTarget
{
    public class TrackIntVariable : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private IntVariable variable;
        [SerializeField] private int padding;

        public void UpdateText()
        {
            text.text = (padding + variable.value).ToString();
        }
    }
}
