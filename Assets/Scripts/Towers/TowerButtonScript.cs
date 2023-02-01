using UnityEngine;
namespace TowerDefense
{
    public class TowerButtonScript : MonoBehaviour
    {
        [SerializeField]
        GameObject _towerObject;

        public GameObject TowerObject
        {
            get
            {

                return _towerObject;
            }
        }
    }
}