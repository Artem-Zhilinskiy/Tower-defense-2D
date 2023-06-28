using UnityEngine;
using System.Collections;

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
        [SerializeField]
        private int _health;
        [SerializeField]
        private byte _rewardAmount;

        private int _target = 0;
        private Transform _enemy;
        private Collider2D _enemyCollider;
        private float _navigationTime;
        bool _isDead = false;
        [SerializeField]
        private float _timeBetweenSteps = 0.02f;

        public bool isDead
        {
            get
            {
                return _isDead;
            }
        }


        /*
        public delegate void CrossPointDelegate();

        public static event CrossPointDelegate CrossPointEvent;

        public void CrossPointEventMethod()
        {
            if (CrossPointEvent != null)
            {
                CrossPointEvent();
            }
        }
        private void CrossPointEventHandler()
        {
            CrossPointEvent += CrossPointMethod;
        }
        */

        Coroutine _enemyMovementCoroutine = null;

        /*
        private void CrossPointMethod()
        {

            if (_wayPoints != null && _isDead == false)
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
        */

        private IEnumerator EnemyMovementCoroutine()
        {
            while (_isDead == false)
            {
                if (_wayPoints != null && _isDead == false)
                {
                    _navigationTime += Time.deltaTime;
                    if (_navigationTime > _navigation)
                    {
                        if (_target < _wayPoints.Length)
                        {
                            _enemy.position = Vector2.MoveTowards(_enemy.position, _wayPoints[_target].position, _navigationTime);
                            yield return new WaitForSecondsRealtime(_timeBetweenSteps);
                        }
                        else
                        {
                            _enemy.position = Vector2.MoveTowards(_enemy.position, _exit.position, _navigationTime);
                            yield return new WaitForSecondsRealtime(_timeBetweenSteps);
                        }
                        _navigationTime = 0;
                    }
                }
            }
            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }


        private void Start()
        {
            _enemy = GetComponent<Transform>();
            _enemyCollider = GetComponent<Collider2D>();
            ManagerScript.Instance.RegisterEnemy(this);

            //Включение корутины движения
            _enemyMovementCoroutine = StartCoroutine(EnemyMovementCoroutine());

            //Включение события прохождения контрольной точки
            //CrossPointEventHandler();
        }

        private void Update()
        {
            /*
            if (_wayPoints != null && _isDead == false)
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
            */
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "MovingPoints")
            {
                _target += 1;
            }
            else if (collision.tag == "Exit")
            {
                ManagerScript.Instance.RoundEscaped += 1;
                ManagerScript.Instance.TotalEscaped += 1;
                ManagerScript.Instance.UnregisterEnemy(this);
                ManagerScript.Instance.IsWaveOver();
            }
            else if (collision.tag == "Projectile")
            {
                ProjectileScript _newP = collision.gameObject.GetComponent<ProjectileScript>();
                EnemyHit(_newP.AttackDamage);
                Destroy(collision.gameObject);
            }
        }

        public void EnemyHit(int _hitPoints)
        {
            if (_health - _hitPoints > 0)
            {
                _health -= _hitPoints;
                ManagerScript.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Hit);
                //anim hurt
            }
            else
            {
                Die();
            }
        }

        public void Die()
        {
            _isDead = true;
            StopCoroutine(EnemyMovementCoroutine());
            _enemyCollider.enabled = false;
            ManagerScript.Instance.TotalKilled += 1;
            ManagerScript.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Death);
            ManagerScript.Instance.AddMoney(_rewardAmount);
            ManagerScript.Instance.IsWaveOver();
        }
    }
}