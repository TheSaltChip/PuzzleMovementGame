using System.IO;
using UnityEngine;

namespace UI.ThrowOnTarget
{
    public class PopulateContent : MonoBehaviour
    {
        [SerializeField] private GameObject template;
        
        public void Populate()
        {
            foreach (var referenceToLevelFile in gameObject.GetComponentsInChildren<ReferenceToLevelFile>())
            {
                Destroy(referenceToLevelFile.gameObject);
            }

            var fileNames = Directory.GetFiles(Constants.PathNames.Throwable, "*.dat");

            foreach (var fileName in fileNames)
            {
                var option = Instantiate(template, transform, false);
                var referenceToLevelFile = option.GetComponent<ReferenceToLevelFile>();

                referenceToLevelFile.FileName = fileName;
            }
        }

        private void Awake()
        {
            Populate();
        }
    }
}