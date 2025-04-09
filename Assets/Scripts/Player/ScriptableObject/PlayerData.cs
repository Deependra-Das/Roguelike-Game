using UnityEngine;

namespace Roguelike.Player
{
    [System.Serializable]
    public class PlayerData
    {
        public int ID;
        public PlayerView playerPrefab;
        public Sprite playerImage;
        public string characterName;
        public Vector3 spawnPosition;
        public Vector3 spawnRotation;
        public float movementSpeed;
        public int maxHealth;
        public float immunityDuration;
    }
}
