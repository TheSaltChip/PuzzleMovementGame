using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace Lobby
{
    public class SelectedLevelSetup : MonoBehaviour
    {
        [SerializeField] private SceneInfo sceneInfo;

        private readonly Regex _pattern = new("[\n ]");

        public void SetSceneName()
        {
            var text = gameObject.GetComponentInChildren<TMP_Text>().text;

            text = _pattern.Replace(text, "");
            
            print(text);
            sceneInfo.sceneName = text;
        }
    }
}