using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
        [SerializeField]
        private TMP_Text _resolutionLabel;

        public static bool _loading = false; //static variable defines if player chose to load game

        public List<ResItem> _resolutions = new List<ResItem>();
        private int _selectedResolution;

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

            bool _foundRes = false;
            for (int i = 0; i < _resolutions.Count; i++)
            {
                if (Screen.width == _resolutions[i]._horizontal && Screen.height == _resolutions[i]._vertical)
                {
                    _foundRes = true;
                    _selectedResolution = i;
                    UpdateResLabel();
                }
            }

            if (!_foundRes)
            {
                ResItem _newRes = new ResItem();
                _newRes._horizontal = Screen.width;
                _newRes._vertical = Screen.height;
                _resolutions.Add(_newRes);
                _selectedResolution = _resolutions.Count - 1;
                UpdateResLabel();
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

        public void ResLeft()
        {
            _selectedResolution--;
            if (_selectedResolution < 0)
            {
                _selectedResolution = 0;
            }
            UpdateResLabel();
        }

        public void ResRight()
        {
            _selectedResolution++;
            if (_selectedResolution > _resolutions.Count - 1)
            {
                _selectedResolution = _resolutions.Count - 1;
            }
            UpdateResLabel();
        }

        public void UpdateResLabel()
        {
            _resolutionLabel.text = _resolutions[_selectedResolution]._horizontal.ToString() + "X" + _resolutions[_selectedResolution]._vertical.ToString();
        }

        public void ApplyGraphics()
        {
            //Screen.fullScreen = _fullScreenToggle.isOn;

            if (_vsyncToggle.isOn)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }

            Screen.SetResolution(_resolutions[_selectedResolution]._horizontal, _resolutions[_selectedResolution]._vertical, _fullScreenToggle.isOn);
        }
    }

    [System.Serializable]
    public class ResItem
    {
        public int _horizontal, _vertical;
    }
}