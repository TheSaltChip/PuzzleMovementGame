using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Lobby
{
    public class LevelInfoManager : MonoBehaviour
    {
        [SerializeField] private SceneInfo sceneInfo;
        [SerializeField] private GameObject buttonPrefab;
        private string _filePath;
        void Awake()
        {
            LoadLevelNames();
        }
    
        public void LoadLevelNames()
        {
        
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var vertLayout = gameObject.GetComponent<VerticalLayoutGroup>();
            print(sceneCount);
            for (var i = 1; i < sceneCount; i++)
            {
                var sceneName = SceneUtility.GetScenePathByBuildIndex(i)
                    .Split('/')
                    .Last()
                    .Split('.')[0];
                var button = GameObject.Instantiate(buttonPrefab,gameObject.transform);
                button.SetActive(true);
                button.GetComponentInChildren<TMP_Text>().text = sceneName;
            }

        
        }
    }
}
