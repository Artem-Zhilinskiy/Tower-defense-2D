using UnityEngine;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        public int _target;
        public Transform _exit;
        public Transform[] _wayPoints;
        public float _navigation;

        private Transform _enemy;
        float _navigationTime;

        private void Start()
        {
            _enemy = GetComponent<Transform>();
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
            Debug.Log(collision.tag);
            if (collision.tag == "MovingPoints")
            {
                _target += 1;
            }
            else if (collision.tag == "Exit")
            {
                Debug.Log("Destroyed");
                ManagerScript._instance.RemoveEnemyFromScreen();
                Destroy(gameObject);
            }
        }
    }
}