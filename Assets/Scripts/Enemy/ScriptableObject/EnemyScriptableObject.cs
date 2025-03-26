using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.Enemy
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/EnemyScriptableObject")]
    public class EnemyScriptableObject : ScriptableObject
    {
        public EnemyType enemyType;
        public GameObject levelPrefab;
        public Image enemyImage;
        public string enemyName;
        public Vector3 spawnPosition;
        public Vector3 spawnRotation;
        public float movementSpeed;
        public int maxHealth;
    }
}

