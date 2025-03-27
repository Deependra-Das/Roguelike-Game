using UnityEngine;

namespace Roguelike.Enemy
{
    public class ShooterEnemyController : EnemyController
    {
        public ShooterEnemyController(EnemyScriptableObject enemySO) : base(enemySO)
        {
        }

        protected override void Attack() 
        {
            if (Time.time - lastAttackTime >= _enemyModel.AttackCooldown)
            {
                lastAttackTime = Time.time;
                ShootProjectilesIn4Directions();
            }
        }

        private void ShootProjectilesIn4Directions()
        {
            Vector2[] directions = new Vector2[]
            { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

            Debug.Log("Projectile Shot");
        }
    }
}

