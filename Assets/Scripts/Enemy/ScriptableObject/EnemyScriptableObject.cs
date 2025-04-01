using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.Enemy
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemyScriptableObject")]
    public class EnemyScriptableObject : ScriptableObject
    {
        public int enemyID;
        public EnemyType enemyType;
        public EnemyView enemyPrefab;
        public Sprite enemyImage;
        public string enemyName;
        public Vector3 spawnPosition;
        public Vector3 spawnRotation;
        public float movementSpeed;
        public int health;
        public int attackPower;
        public float attackCooldown;
        public int ExpDrop;

        public int numberOfProjectiles;
        public int projectileDamage;
        public float projectileRadius;
        public float projectileLifeTime;
        public float projectileSpeed;
    }
}

