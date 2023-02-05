using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
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
            if (Input.GetMouseButton(0))
            {
                Vector2 _mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D _hit = Physics2D.Raycast(_mousePoint, Vector2.zero);

                if (_hit.collider.tag  == "TowerSite")
                {
                    PlaceTower(_hit);
                }
            }
        }

        public void PlaceTower(RaycastHit2D _hit)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && _towerButtonIsPressed!= null)
            {
                GameObject _newTower = Instantiate(_towerButtonIsPressed.TowerObject);
                _newTower.transform.position = _hit.transform.position;
            }
        }

        public void SelectedTower(TowerButtonScript _towerSelected)
        {
            _towerButtonIsPressed = _towerSelected;
            Debug.Log("Pressed " + _towerButtonIsPressed.gameObject);
        }
    }
}