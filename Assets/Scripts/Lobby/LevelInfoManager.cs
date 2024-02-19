using System.Linq;
using UI;
using UI.Lobby;
using UnityEngine;
using UnityEngine.Localization.Components;
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
            for (var i = 1; i < sceneCount; i++)
            {
                var sceneName = SceneUtility.GetScenePathByBuildIndex(i)
                    .Split('/')
                    .Last()
                    .Split('.')[0];

                var button = Instantiate(buttonPrefab, gameObject.transform);
                button.SetActive(true);
                var localizeStringEvent = button.GetComponentInChildren<LocalizeStringEvent>();
                localizeStringEvent.SetTable("Lobby");
                localizeStringEvent.SetEntry(sceneName);
                
                var highlightButton = button.GetComponentInChildren<HighlightButtonIfSelected>();
                highlightButton.SetId(i.ToString());

                var hoverActions = button.GetComponentInChildren<LevelButtonHoverActions>();
                hoverActions.SetStringEntry(sceneName + "Info");
            }
        }
    }
}