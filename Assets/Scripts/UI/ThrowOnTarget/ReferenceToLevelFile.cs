using System.IO;
using TMPro;
using UnityEngine;

namespace UI.ThrowOnTarget
{
    public class ReferenceToLevelFile : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                text.text = _filePath.Split(Path.DirectorySeparatorChar)[^1].Split('.')[0];
            }
        }
    }
}