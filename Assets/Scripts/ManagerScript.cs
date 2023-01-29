using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class ManagerScript : MonoBehaviour
    {
        public static ManagerScript _instance = null;

        public GameObject _spawnPoint;
        public GameObject[] _enemies;
        public int _maxEnemiesOnScreen;
        public int _totalEnemies;
        public int _enemiesPerSpawn;

        private int _enemiesOnScreen = 0;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Spawn()
        {
            if (_enemiesPerSpawn > 0 && _enemiesOnScreen < _totalEnemies)
            {
                for (int i = 0; i< _enemiesPerSpawn; i++)
                {
                    if (_enemiesOnScreen < _maxEnemiesOnScreen)
                    {
                        GameObject _newEnemy = Instantiate(_enemies[0]) as GameObject;
                        _newEnemy.transform.position = _spawnPoint.transform.position;
                        _enemiesOnScreen += 1;
                    }
                }
            }
        }

        private void Start()
        {
            Spawn();
        }

        public void RemoveEnemyFromScreen()
        {
            if (_enemiesOnScreen > 0)
            {
                _enemiesOnScreen -= 1;
            }
        }

    }
}