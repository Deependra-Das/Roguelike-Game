using Roguelike.Main;
using Roguelike.Projectile;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
                ShootProjectiles();
            }
        }

        private void ShootProjectiles()
        {
            for (int i = 0; i < _enemyModel.NumberOfProjectiles; i++)
            {
                float angle = (i * 360f) / _enemyModel.NumberOfProjectiles;
                Vector3 direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * _enemyModel.ProjectileRadius, Mathf.Sin(Mathf.Deg2Rad * angle) * _enemyModel.ProjectileRadius, 0f);
                GameService.Instance.GetService<ProjectileService>().SpawnProjectile(ProjectileType.EnemyOrb, new Vector2(_enemyView.gameObject.transform.position.x, _enemyView.gameObject.transform.position.y+1),
                    direction, _enemyModel.ProjectileDamage, _enemyModel.ProjectileLifeTime, _enemyModel.ProjectileSpeed);
            }
        }
    }
}

