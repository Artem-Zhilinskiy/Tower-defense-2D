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

        public List<Enemy> EnemyList = new List<Enemy>();

        private const float _spawnDelay = 0.5f;

        private IEnumerator Spawn()
        {
            if (_enemiesPerSpawn > 0 && EnemyList.Count < _totalEnemies)
            {
                for (int i = 0; i< _enemiesPerSpawn; i++)
                {
                    if (EnemyList.Count < _maxEnemiesOnScreen)
                    {
                        GameObject _newEnemy = Instantiate(_enemies[2]) as GameObject;
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

        private void Start()
        {
            StartCoroutine(Spawn());
        }
    }
}