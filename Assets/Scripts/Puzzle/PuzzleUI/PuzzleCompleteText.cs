using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using Variables;

namespace Puzzle.PuzzleUI
{
    public class PuzzleCompleteText : MonoBehaviour
    {
        [SerializeField] private BoolVariable state;

        public void SetUpText()
        {
            var local = gameObject.GetComponent<LocalizeStringEvent>();
            if (state.value)
            {
                var loc = new LocalizedString
                {
                    TableReference = "PuzzleUI",
                    TableEntryReference = "Correct"
                };
                local.StringReference = loc;
            }
            else
            {
                /*var loc = new LocalizedString
            {
                TableReference = "PuzzleUI",
                TableEntryReference = "Incorrect"
            };*/
                local.SetEntry("Incorrect");
                //local.StringReference = loc;
            }
        
        }
    }
}
