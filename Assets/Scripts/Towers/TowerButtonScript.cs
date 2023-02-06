using UnityEngine;
namespace TowerDefense
{
    public class TowerButtonScript : MonoBehaviour
    {
        [SerializeField]
        GameObject _towerObject;
        [SerializeField]
        Sprite _dragSprite;

        public GameObject TowerObject
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
    }
}