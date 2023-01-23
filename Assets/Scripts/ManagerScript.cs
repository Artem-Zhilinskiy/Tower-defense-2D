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
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}