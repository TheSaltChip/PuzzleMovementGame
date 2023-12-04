using TMPro;
using UnityEngine;
using Variables;

namespace UI.ThrowOnTarget
{
    public class TrackIntVariable : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private IntVariable variable;
        [SerializeField] private int offset;

        public void UpdateText()
        {
            text.text = (offset + variable.value).ToString();
        }
    }
}
