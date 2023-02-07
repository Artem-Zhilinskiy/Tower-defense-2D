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
        private ProjectileScript _projectile;
        private Enemy _targetEnemy = null;
        private float _attackCounter;

        private List<Enemy> GetEnemiesInRange()
        {
            List<Enemy> _enemiesInRange = new List<Enemy>();

            foreach (Enemy enemy in ManagerScript.Instance.EnemyList)
            {
                if (Vector2.Distance(transform.position, enemy.transform.position) <= _attackRadius)
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
                if (Vector2.Distance(transform.position, enemy.transform.position) < _smallestDistance)
                {
                    _smallestDistance = Vector2.Distance(transform.position, enemy.transform.position);
                    _nearestEnemy = enemy;
                }
            }
            return _nearestEnemy;
        }
    }
}