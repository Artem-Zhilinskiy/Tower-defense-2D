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
        private Button _continueGameButtonMain;
        [SerializeField]
        private Button _exitButtonMain;

        public void NewGame()
        {
            SceneManager.LoadScene(1);
            PlayerPrefs.SetInt("Level", 1);
        }

        public void ContinueGame()
        {
            /*
            switch (PlayerPrefs.GetInt("Level"))
            {
                case 2:
                    SceneManager.LoadScene("Level2Scene");
                    break;
                case 3:
                    SceneManager.LoadScene("Level3Scene");
                    break;
                case 4:
                    SceneManager.LoadScene("Level4Scene");
                    break;
                default:
                    Debug.Log("Сохранений нет. Начните новую игру");
                    break;
            }
            */
        }

        public void QuitGame()
        {
            Debug.Log("Выход из игры");
            Application.Quit();
        }
    }
}