using System.Collections;
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
        private Button _exitButtonMain;

        public void NewGame()
        {
            SceneManager.LoadScene("MainScene");
        }

        public void QuitGame()
        {
            Debug.Log("Выход из игры");
            Application.Quit();
        }
    }
}