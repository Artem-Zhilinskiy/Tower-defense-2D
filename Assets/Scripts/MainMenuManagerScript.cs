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
        [Header("Кнопки главного меню")]
        [SerializeField]
        private Button _newGameButtonMain;
        [SerializeField]
        private Button _continueGameButtonMain;
        [SerializeField]
        private Button _exitButtonMain;

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
    }
}