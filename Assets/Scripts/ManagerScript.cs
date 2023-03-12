using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public enum gameStatus
    {
        next, play, gameover, win
    }

    public class ManagerScript : Loader<ManagerScript>
    {
        [SerializeField]
        private int _totalWaves = 10;
        [SerializeField]
        private Text _totalMoneyLabel;
        [SerializeField]
        private Text _currentWave;
        [SerializeField]
        private Text _totalEscapedLabel;
        [SerializeField]
        private Text _playButtonLabel;
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private GameObject _spawnPoint;
        [SerializeField]
        private Enemy[] _enemies;
        [SerializeField]
        private int _totalEnemies = 3;
        [SerializeField]
        private int _enemiesPerSpawn;

        [SerializeField]
        private GameObject _pauseMenuUI;
        private static bool _gameIsPaused = false;

        [SerializeField]
        private Button _nextLevelButton;

        private int _waveNumber = 0;
        private int _totalMoney = 10;
        private int _totalEscaped = 0;
        private int _roundEscaped = 0;
        private int _totalKilled = 0;
        private int _whichEnemiesToSpawn = 0;
        private int _enemiesToSpawn = 0;
        private gameStatus _currentState = gameStatus.play;
        private AudioSource _audioSource;

        public List<Enemy> EnemyList = new List<Enemy>();

        private const float _spawnDelay = 0.5f;

        public int TotalEscaped
        {
            get
            {
                return _totalEscaped;
            }
            set
            {
                _totalEscaped = value;
            }
        }

        public int RoundEscaped
        {
            get
            {
                return _roundEscaped;
            }
            set
            {
                _totalEscaped = value;
            }
        }

        public int TotalKilled
        {
            get
            {
                return _totalKilled;
            }
            set
            {
                _totalKilled = value;
            }

        }

        public int TotalMoney
        {
            get
            {
                return _totalMoney;
            }
            set
            {
                _totalMoney = value;
                _totalMoneyLabel.text = TotalMoney.ToString();
            }
        }

        public AudioSource AudioSource
        {
            get
            {
                return _audioSource;
            }
        }

        private void Start()
        {
            _playButton.gameObject.SetActive(false);
            _audioSource = GetComponent<AudioSource>();
            ShowMenu();
        }

        private void Update()
        {
            HandleMouseRightClick();
            PauseMenu();
        }

        private IEnumerator Spawn()
        {
            if (_enemiesPerSpawn > 0 && EnemyList.Count < _totalEnemies)
            {
                for (int i = 0; i< _enemiesPerSpawn; i++)
                {
                    if (EnemyList.Count < _totalEnemies)
                    {
                        Enemy _newEnemy = Instantiate(_enemies[Random.Range(0, _enemiesToSpawn)]) as Enemy;
                        _newEnemy.transform.position = _spawnPoint.transform.position;
                    }
                }

                yield return new WaitForSeconds(_spawnDelay);
                StartCoroutine(Spawn());
            }
        }

        public void RegisterEnemy(Enemy enemy)
        {
            EnemyList.Add(enemy);
        }

        public void UnregisterEnemy(Enemy enemy)
        {
            EnemyList.Remove(enemy);
            Destroy(enemy.gameObject);
        }

        public void DestroyEnemies()
        {
            foreach (Enemy enemy in EnemyList)
            {
                Destroy(enemy.gameObject);
            }
            EnemyList.Clear();
        }

        public void AddMoney(int amount)
        {
            TotalMoney += amount;
        }

        public void SubstractMoney(int amount)
        {
            TotalMoney -= amount;
        } 

        public void IsWaveOver()
        {
            _totalEscapedLabel.text = "Escaped " + TotalEscaped + " /10";

            if ((RoundEscaped + TotalKilled) == _totalEnemies)
            {
                if (_waveNumber <= _enemies.Length)
                {
                    _enemiesToSpawn = _waveNumber;
                }
                SetCurrentGameState();
                ShowMenu();
            }
        }

        public void SetCurrentGameState()
        {
            if (_totalEscaped >= 10)
            {
                _currentState = gameStatus.gameover;
            }
            else if (_waveNumber == 0 && (RoundEscaped + TotalKilled) == 0)
            {
                _currentState = gameStatus.play;
            }
            else if (_waveNumber >= _totalWaves)
            {
                _currentState = gameStatus.win;
            }
            else
            {
                _currentState = gameStatus.next;
            }
        }

        public void PlayButtonPressed()
        {
            switch(_currentState)
            {
                case gameStatus.next:
                    _waveNumber += 1;
                    _totalEnemies += _waveNumber;
                    break;

                default:
                    _totalEnemies = 3;
                    _totalEscaped = 0;
                    _totalMoney = 50;
                    _enemiesToSpawn = 0;
                    TowerManagerScript.Instance.DestroyAllTowers();
                    TowerManagerScript.Instance.RenameBuildTagSite();
                    _totalMoneyLabel.text = TotalMoney.ToString();
                    _totalEscapedLabel.text = "Escaped " + _totalEscaped + " /10";
                    _audioSource.PlayOneShot(SoundManager.Instance.NewGame);
                    break;
            }
            DestroyEnemies();
            TotalKilled = 0;
            RoundEscaped = 0;
            _currentWave.text = "Wave " + (_waveNumber + 1);
            StartCoroutine(Spawn());
            _playButton.gameObject.SetActive(false);
        }

        public void ShowMenu()
        {
            switch (_currentState)
            {
                case gameStatus.gameover:
                    _playButtonLabel.text = "Play again";
                    AudioSource.PlayOneShot(SoundManager.Instance.GameOver);
                    break;
                case gameStatus.next:
                    _playButtonLabel.text = "Next Wave";
                    break;
                case gameStatus.play:
                    _playButtonLabel.text = "Play game";
                    break;
                case gameStatus.win:
                    _playButtonLabel.text = "You win";
                    break;
            }
            _playButton.gameObject.SetActive(true);
        }

        private void HandleMouseRightClick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                TowerManagerScript.Instance.DisableDrag();
                TowerManagerScript.Instance._towerButtonIsPressed = null;
            }
        }

        private void PauseMenu()
        {
            if (Keyboard.current[Key.Escape].wasPressedThisFrame)
            {
                Pause();
            }
        }
        //Открытие меню-паузы на кнопку Escape
        private void Pause()
        {
            _gameIsPaused = true;
            _pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Continue()
        {
            _gameIsPaused = false;
            _pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
        /*
        public void NextLevel()
        {
            string _currentScene = SceneManager.GetActiveScene().name;
            switch (_currentScene)
            {
                case "Level1Scene":
                    SceneManager.LoadScene("Level2Scene");
                    PlayerPrefs.SetInt("Level", 2);
                    break;
                case "Level2Scene":
                    SceneManager.LoadScene("Level3Scene");
                    PlayerPrefs.SetInt("Level", 3);
                    break;
                case "Level3Scene":
                    SceneManager.LoadScene("Level4Scene");
                    PlayerPrefs.SetInt("Level", 4);
                    break;
            }
        }
        */

        public void SaveGame()
        {
            Debug.Log("Save game button is triggered");
        }

        public void ExitButton()
        {
            Application.Quit();
            Debug.Log("Exit button is triggered");
        }

    }
}