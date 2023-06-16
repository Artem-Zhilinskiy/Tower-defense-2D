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
        [SerializeField]
        private Toggle _fullScreenToggle;
        [SerializeField]
        private Toggle _vsyncToggle;

        public static bool _loading = false; //static variable defines if player chose to load game

        private void Start()
        {
            _fullScreenToggle.isOn = Screen.fullScreen;

            if (QualitySettings.vSyncCount == 0)
            {
                _vsyncToggle.isOn = false;
            }
            else
            {
                _vsyncToggle.isOn = true;
            }
        }

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

        public void ApplyGraphics()
        {
            Screen.fullScreen = _fullScreenToggle.isOn;

            if (_vsyncToggle.isOn)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }
        }
    }
}