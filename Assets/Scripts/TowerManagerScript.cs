using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TowerManagerScript : Loader<TowerManagerScript>
    {
        private TowerButtonScript _towerButtonIsPressed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SelectedTower(TowerButtonScript _towerSelected)
        {
            _towerButtonIsPressed = _towerSelected;
            Debug.Log("Pressed " + _towerButtonIsPressed.gameObject);
        }
    }
}