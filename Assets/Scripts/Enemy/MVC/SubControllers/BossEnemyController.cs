using UnityEngine;

namespace Roguelike.Enemy
{
    public class BossEnemyController : EnemyController
    {
        public BossEnemyController(EnemyScriptableObject enemySO) : base(enemySO)
        {
        }

        protected override void Attack()
        {
            if (Time.time - lastAttackTime >= _enemyModel.AttackCooldown)
            {
                lastAttackTime = Time.time;
                ShootProjectilesIn8Directions();
            }
        }

        private void ShootProjectilesIn8Directions()
        {
            Vector2[] directions = new Vector2[]
            {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right,
            new Vector2(1, 1).normalized, new Vector2(1, -1).normalized,
            new Vector2(-1, 1).normalized, new Vector2(-1, -1).normalized
            };

            Debug.Log("Projectile Shot");
        }

    }
}

