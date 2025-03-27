using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Roguelike.Enemy
{
    public class BomberEnemyController : EnemyController
    {
        public BomberEnemyController(EnemyScriptableObject enemySO) : base(enemySO)
        {
        }

        protected override void Attack() {}

        public override void OnCollisionWithPlayer()
        {
            isDead = true;
            OnEnemyDeath();
        }
    }
}
