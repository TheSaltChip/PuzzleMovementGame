using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class LevelInfoManager : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;

        private void Awake()
        {
            LoadLevelNames();
        }

        public void LoadLevelNames()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;

            for (var i = 2; i < sceneCount; i++)
            {
                var sceneName = SceneUtility.GetScenePathByBuildIndex(i)
                    .Split('/')
                    .Last()
                    .Split('.')[0];

                var button = Instantiate(buttonPrefab, gameObject.transform);
                button.SetActive(true);
                button.GetComponentInChildren<TMP_Text>().text = sceneName;
            }
        }
    }
}