using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace TowerDefense
{
    public class TowerManagerScript : Loader<TowerManagerScript>
    {
        private TowerButtonScript _towerButtonIsPressed;

        SpriteRenderer _spriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 _mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D _hit = Physics2D.Raycast(_mousePoint, Vector2.zero);

                if (_hit.collider != null && _hit.collider.tag  == "TowerSite")
                {
                    _hit.collider.tag = "TowerSiteFull";
                    PlaceTower(_hit);
                }
            }
            if (_spriteRenderer.enabled)
            {
                FollowMouse();
            }
        }

        public void PlaceTower(RaycastHit2D _hit)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && _towerButtonIsPressed!= null)
            {
                GameObject _newTower = Instantiate(_towerButtonIsPressed.TowerObject);
                _newTower.transform.position = _hit.transform.position;
                DisableDrag();

                //disable tower placement if done once
                _towerButtonIsPressed = null;
            }
        }

        public void SelectedTower(TowerButtonScript _towerSelected)
        {
            _towerButtonIsPressed = _towerSelected;
            EnableDrag(_towerButtonIsPressed.DragSprite);
            //Debug.Log("Pressed " + _towerButtonIsPressed.gameObject);
        }

        public void FollowMouse()
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(transform.position.x, transform.position.y);
        }

        public void EnableDrag(Sprite sprite)
        {
            _spriteRenderer.enabled = true;
            _spriteRenderer.sprite = sprite;
        }

        public void DisableDrag()
        {
            _spriteRenderer.enabled = false;
        }
    }
}