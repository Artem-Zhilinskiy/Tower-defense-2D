using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Loader : MonoBehaviour
    {
        public GameObject _manager;

        private void Awake()
        {
            if (ManagerScript._instance == null)
            {
                Instantiate(_manager);
            }
        }

    }
}