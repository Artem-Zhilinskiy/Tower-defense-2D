using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class TowerControlScript : MonoBehaviour
    {
        [SerializeField]
        private float _timeBetweenAttack;
        [SerializeField]
        private float _attackRadius;
        [SerializeField]
        private ProjectileScript _projectile;
        private Enemy _targetEnemy = null;
        private float _attackCounter;
        bool _isAttacking = false;

        private void Update()
        {
            _attackCounter -= Time.deltaTime;

            if (_targetEnemy == null || _targetEnemy.isDead)
            {
                Enemy _nearestEnemy = GetNearestEnemy();
                if (_nearestEnemy != null && Vector2.Distance(transform.localPosition, _nearestEnemy.transform.localPosition) <= _attackRadius)
                {
                    _targetEnemy = _nearestEnemy;
                }
            }
            else
            {
                if (_attackCounter <= 0)
                {
                    _isAttacking = true;
                    _attackCounter = _timeBetweenAttack;
                }
                else
                {
                    _isAttacking = false;
                }
            }
            
            if (Vector2.Distance(transform.localPosition, _targetEnemy.transform.localPosition) > _attackRadius)
            {
                _targetEnemy = null;
            }
        }

        public void FixedUpdate()
        {
            if (_isAttacking == true)
            {
                Attack();
            }
        }

        public void Attack()
        {
            _isAttacking = false;
            ProjectileScript _newProjectile = Instantiate(_projectile) as ProjectileScript;
            _newProjectile.transform.localPosition = transform.localPosition;

            if (_targetEnemy == null)
            {
                Destroy(_newProjectile);
            }
            else
            {
                //move projectile to enemy
                StartCoroutine(MoveProjectile(_newProjectile));
            }
        }

        private IEnumerator MoveProjectile(ProjectileScript _projectile)
        {
            while (GetTargetDistance(_targetEnemy) > 0.20f && _projectile != null && _targetEnemy != null)
            {
                var _direction = _targetEnemy.transform.localPosition - transform.localPosition;
                var _angleDirection = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                _projectile.transform.rotation = Quaternion.AngleAxis(_angleDirection, Vector3.forward);
                _projectile.transform.localPosition = Vector2.MoveTowards(_projectile.transform.localPosition, _targetEnemy.transform.localPosition, 5f *Time.deltaTime);
                yield return null;
            }

            if (_projectile != null || _targetEnemy == null)
            {
                Destroy(_projectile);
            }
        }

        private float GetTargetDistance(Enemy _thisEnemy)
        {
            if (_thisEnemy == null)
            {
                _thisEnemy = GetNearestEnemy();
                if (_thisEnemy = null)
                {
                    return 0f;
                }
            }
            return Mathf.Abs(Vector2.Distance(transform.localPosition, _thisEnemy.transform.localPosition));
        }

        private List<Enemy> GetEnemiesInRange()
        {
            List<Enemy> _enemiesInRange = new List<Enemy>();

            foreach (Enemy enemy in ManagerScript.Instance.EnemyList)
            {
                if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= _attackRadius)
                {
                    _enemiesInRange.Add(enemy);
                }
            }
            return _enemiesInRange;
        }

        private Enemy GetNearestEnemy()
        {
            Enemy _nearestEnemy = null;
            float _smallestDistance = float.PositiveInfinity;

            foreach (Enemy enemy in GetEnemiesInRange())
            {
                if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < _smallestDistance)
                {
                    _smallestDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                    _nearestEnemy = enemy;
                }
            }
            return _nearestEnemy;
        }
    }
}