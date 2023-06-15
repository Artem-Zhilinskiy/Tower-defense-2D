using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenuManagerScript : MonoBehaviour
    {
        [Header("Панель главного меню")]
        [SerializeField]
        private GameObject _mainMenuPanel;
        [SerializeField]
        private Button _newGameButtonMain;
        [SerializeField]
        private Button _continueGameButtonMain;
        [SerializeField]
        private Button _exitButtonMain;

        [Header("Панель настроек разрешения")]
        [SerializeField]
        private GameObject _preferencesPanel;
        [SerializeField]
        private Button _leftResButton;
        [SerializeField]
        private Button _rightResButton;
        [SerializeField]
        private Button _applyResButton;
        [SerializeField]
        private Button _closeResButton;

        public static bool _loading = false; //static variable defines if player chose to load game

        public void NewGame()
        {
            SceneManager.LoadScene(1);
            //PlayerPrefs.SetInt("Level", 1);
        }

        public void ContinueGame()
        {
            Debug.Log("Continue game button is triggered");
            _loading = true;
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            Debug.Log("Выход из игры");
            Application.Quit();
        }

        public void OpenPreferencesMenu()
        {
            _mainMenuPanel.SetActive(false);
            _preferencesPanel.SetActive(true);
        }

        public void ClosePreferenceMenu()
        {
            _preferencesPanel.SetActive(false);
            _mainMenuPanel.SetActive(true);
        }
    }
}