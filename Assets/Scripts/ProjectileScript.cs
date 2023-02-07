using UnityEngine;

public enum projectileType
{
    projectile1,
    projectile2,
    projectile3
};

namespace TowerDefense
{
    public class ProjectileScript : MonoBehaviour
    {
        [SerializeField]
        private int _attackDamage;

        [SerializeField]
        projectileType _pType;

        public int AttackDamage
        {
            get
            {

                return _attackDamage;
            }
        }

        public projectileType PType
        {
            get
            {

                return _pType;
            }
        }
    } 
}