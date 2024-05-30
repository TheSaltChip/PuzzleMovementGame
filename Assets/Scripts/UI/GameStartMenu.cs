#region License
// Copyright (C) 2024 Sebastian Misje Jonassen & Mathias Nupen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Commons Clause License version 1.0 with GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Commons Clause License and GNU General Public License for more details.
// 
// You should have received a copy of the Commons Clause License and GNU General Public License
// along with this program.  If not, see <https://commonsclause.com/> and <https://www.gnu.org/licenses/>.
#endregion

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

        public UnityEvent onGameStart; 

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
            onGameStart?.Invoke();
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