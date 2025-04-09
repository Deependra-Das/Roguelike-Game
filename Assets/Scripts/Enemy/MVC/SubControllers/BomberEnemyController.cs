using UnityEngine;

namespace Roguelike.Enemy
{
    public class BomberEnemyController : EnemyController
    {
        public BomberEnemyController(EnemyScriptableObject enemySO) : base(enemySO) {}

        public override void OnCollisionWithPlayer()
        {
            isDead = true;
            OnEnemyDeath();
        }
    }
}
