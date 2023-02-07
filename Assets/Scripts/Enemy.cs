using UnityEngine;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {


        [SerializeField]
        private Transform _exit;
        [SerializeField]
        private Transform[] _wayPoints;
        [SerializeField]
        private float _navigation;

        private Transform _enemy;
        private float _navigationTime;
        private int _target;

        private void Start()
        {
            _enemy = GetComponent<Transform>();
            ManagerScript.Instance.RegisterEnemy(this);
        }

        private void Update()
        {
            if (_wayPoints != null)
            {
                _navigationTime += Time.deltaTime;
                if (_navigationTime > _navigation)
                {
                    if (_target < _wayPoints.Length)
                    {
                        _enemy.position = Vector2.MoveTowards(_enemy.position, _wayPoints[_target].position, _navigationTime);
                    }   
                    else
                    {
                        _enemy.position = Vector2.MoveTowards(_enemy.position, _exit.position, _navigationTime);
                    }
                    _navigationTime = 0;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "MovingPoints")
            {
                _target += 1;
            }
            else if (collision.tag == "Exit")
            {
                ManagerScript.Instance.UnregisterEnemy(this);
            }
        }
    }
}