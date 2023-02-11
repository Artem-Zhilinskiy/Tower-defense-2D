using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace TowerDefense
{
    public class TowerManagerScript : Loader<TowerManagerScript>
    {
        public TowerButtonScript _towerButtonIsPressed { get; set; }

        SpriteRenderer _spriteRenderer;
        private List<TowerControlScript> TowerList = new List<TowerControlScript>();
        private List<Collider2D> BuildList = new List<Collider2D>();
        private Collider2D _buildTile;

        // Start is called before the first frame update
        void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _buildTile = GetComponent<Collider2D>();
            _spriteRenderer.enabled = false;
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
                    _buildTile = _hit.collider;
                    _buildTile.tag = "TowerSiteFull";
                    RegisterBuildSite(_buildTile);
                    PlaceTower(_hit);
                }
            }
            if (_spriteRenderer.enabled)
            {
                FollowMouse();
            }
        }

        public void RegisterBuildSite(Collider2D buildTag)
        {
            BuildList.Add(buildTag);
        }

        public void RegisterTower (TowerControlScript tower)
        {
            TowerList.Add(tower);
        }

        public void RenameBuildTagSite()
        {
            foreach (Collider2D buildTag in BuildList)
            {
                buildTag.tag = "TowerSite";
            }
            BuildList.Clear();
        }

        public void DestroyAllTowers()
        {
            foreach (TowerControlScript tower in TowerList)
            {
                Destroy(tower.gameObject);
            }
            TowerList.Clear(); 
        }

        public void PlaceTower(RaycastHit2D _hit)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && _towerButtonIsPressed!= null)
            {
                TowerControlScript _newTower = Instantiate(_towerButtonIsPressed.TowerObject);
                _newTower.transform.position = _hit.transform.position;
                BuyTower(_towerButtonIsPressed.TowerPrice);
                ManagerScript.Instance.AudioSource.PlayOneShot(SoundManager.Instance.TowerBuilt);
                RegisterTower(_newTower);
                DisableDrag();

                //disable tower placement if done once
                _towerButtonIsPressed = null;
            }
        }

        public void BuyTower(int price)
        {
            ManagerScript.Instance.SubstractMoney(price);
        }

        public void SelectedTower(TowerButtonScript _towerSelected)
        {
            if (_towerSelected.TowerPrice <= ManagerScript.Instance.TotalMoney)
            {
                _towerButtonIsPressed = _towerSelected;
                EnableDrag(_towerButtonIsPressed.DragSprite);
            }
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