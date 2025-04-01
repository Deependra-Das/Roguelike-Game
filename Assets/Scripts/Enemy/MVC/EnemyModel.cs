using Roguelike.Enemy;
using UnityEngine;

namespace Roguelike.Enemy
{
    public class EnemyModel
    {
        private EnemyController _enemyController;

        public EnemyModel(EnemyScriptableObject enemyDataObj)
        {
            EnemyID = enemyDataObj.enemyID;
            EnemyType = enemyDataObj.enemyType;
            EnemyPrefab = enemyDataObj.enemyPrefab;
            EnemyImage = enemyDataObj.enemyImage;
            EnemyName = enemyDataObj.enemyName;
            SpawnPosition = enemyDataObj.spawnPosition;
            SpawnRotation = enemyDataObj.spawnRotation;
            MovementSpeed = enemyDataObj.movementSpeed;
            Health = enemyDataObj.health;
            AttackPower = enemyDataObj.attackPower;
            AttackCooldown = enemyDataObj.attackCooldown;
            ExpDrop = enemyDataObj.ExpDrop;

            NumberOfProjectiles = enemyDataObj.numberOfProjectiles;
            ProjectileDamage = enemyDataObj.projectileDamage;
            ProjectileRadius = enemyDataObj.projectileRadius;
            ProjectileLifeTime = enemyDataObj.projectileLifeTime;
            ProjectileSpeed = enemyDataObj.projectileSpeed;
        }

        ~EnemyModel() { }

        public void SetController(EnemyController enemyController)
        {
            _enemyController = enemyController;
        }

        public void UpdateHealth(int value)
        {
            Health += value;
        }

        public int EnemyID { get; private set; }
        public EnemyType EnemyType { get; private set; }
        public EnemyView EnemyPrefab { get; private set; }
        public Sprite EnemyImage { get; private set; }
        public string EnemyName { get; private set; }
        public Vector3 SpawnPosition { get; private set; }
        public Vector3 SpawnRotation { get; private set; }
        public float MovementSpeed { get; private set; }
        public int Health { get; private set; }
        public int AttackPower { get; private set; }
        public float AttackCooldown { get; private set; }
        public int ExpDrop { get; private set; }
        public int NumberOfProjectiles { get; private set; }
        public int ProjectileDamage { get; private set; }
        public float ProjectileRadius { get; private set; }
        public float ProjectileLifeTime { get; private set; }
        public float ProjectileSpeed { get; private set; }
    }
}
