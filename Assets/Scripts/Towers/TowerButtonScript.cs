﻿using UnityEngine;
namespace TowerDefense
{
    public class TowerButtonScript : MonoBehaviour
    {
        [SerializeField]
        TowerControlScript _towerObject;
        [SerializeField]
        Sprite _dragSprite;
        [SerializeField]
        private byte _towerPrice;

        public TowerControlScript TowerObject
        {
            get
            {

                return _towerObject;
            }
        }

        public Sprite DragSprite
        {
            get
            {

                return _dragSprite;
            }
        }

        public byte TowerPrice
        {
            get
            {
                return _towerPrice;
            }
        }
    }
}