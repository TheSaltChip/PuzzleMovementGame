using System.Collections.Generic;
using SceneTransition;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameStartMenu : MonoBehaviour
    {
        [Header("UI Pages")] public GameObject mainMenu;

        public GameObject options;

        [Header("Main Menu Buttons")] public Button startButton;

        public Button optionButton;
        public Button quitButton;

        public List<Button> returnButtons;

        // Start is called before the first frame update
        private void Start()
        {
            EnableMainMenu();

            //Hook events
            startButton.onClick.AddListener(StartGame);
            optionButton.onClick.AddListener(EnableOption);
            quitButton.onClick.AddListener(QuitGame);

            foreach (var item in returnButtons) item.onClick.AddListener(EnableMainMenu);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void StartGame()
        {
            HideAll();
            SceneTransitionManager.Instance.LoadScene("PuzzleWorld");
        }

        public void HideAll()
        {
            mainMenu.SetActive(false);
            options.SetActive(false);
        }

        public void EnableMainMenu()
        {
            mainMenu.SetActive(true);
            options.SetActive(false);
        }

        public void EnableOption()
        {
            mainMenu.SetActive(false);
            options.SetActive(true);
        }
    }
}