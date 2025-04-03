using UnityEngine;

namespace Roguelike.Player
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public int ID;
        public PlayerView playerPrefab;
        public string characterName;
        public Vector3 spawnPosition;
        public Vector3 spawnRotation;
        public float movementSpeed;
        public int maxHealth;
        public float immunityDuration;
    }
}
