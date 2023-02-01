using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class ManagerScript : Loader<ManagerScript>
    {
        [SerializeField]
        private GameObject _spawnPoint;
        [SerializeField]
        private GameObject[] _enemies;
        [SerializeField]
        private int _maxEnemiesOnScreen;
        [SerializeField]
        private int _totalEnemies;
        [SerializeField]
        private int _enemiesPerSpawn;


        private int _enemiesOnScreen = 0;

        private const float _spawnDelay = 0.5f;

        private IEnumerator Spawn()
        {
            if (_enemiesPerSpawn > 0 && _enemiesOnScreen < _totalEnemies)
            {
                for (int i = 0; i< _enemiesPerSpawn; i++)
                {
                    if (_enemiesOnScreen < _maxEnemiesOnScreen)
                    {
                        GameObject _newEnemy = Instantiate(_enemies[2]) as GameObject;
                        _newEnemy.transform.position = _spawnPoint.transform.position;
                        _enemiesOnScreen += 1;
                    }
                }

                yield return new WaitForSeconds(_spawnDelay);
                StartCoroutine(Spawn());
            }
        }

        private void Start()
        {
            StartCoroutine(Spawn());
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