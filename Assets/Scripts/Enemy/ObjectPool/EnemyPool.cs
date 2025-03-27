using System;
using UnityEngine;
using Roguelike.Utilities;

namespace Roguelike.Enemy
{
    public class EnemyPool : GenericObjectPool<EnemyController>
    {
        private EnemyScriptableObject _enemyData;

        public EnemyController GetEnemy<T>(EnemyScriptableObject enemyData) where T : EnemyController
        {
            this._enemyData = enemyData;
            return GetItem<T>();
        }

        protected override EnemyController CreateItem<T>()
        {
            if (typeof(T) == typeof(BomberEnemyController))
            {
                return new BomberEnemyController(_enemyData);
            }
            else if (typeof(T) == typeof(StalkerEnemyController))
            {
                return new StalkerEnemyController(_enemyData);
            }
            else if (typeof(T) == typeof(ShooterEnemyController))
            {
                return new ShooterEnemyController(_enemyData);
            }
            else if (typeof(T) == typeof(BossEnemyController))
            {
                return new BossEnemyController(_enemyData);
            }
            else
            {
                throw new NotSupportedException("Enemy type not supported");
            }
        }
    }
}
