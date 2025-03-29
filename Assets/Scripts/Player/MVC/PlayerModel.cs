using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerModel
    {
        private PlayerController _playerController;

        public PlayerModel(PlayerScriptableObject playerDataObj)
        {
            PlayerID = playerDataObj.ID;
            PlayerPrefab = playerDataObj.playerPrefab;
            CharacterName = playerDataObj.characterName;
            SpawnPosition = playerDataObj.spawnPosition;
            SpawnRotation = playerDataObj.spawnRotation;
            MovementSpeed = playerDataObj.movementSpeed;
            MaxHealth = playerDataObj.maxHealth;
            CurrentHealth = playerDataObj.maxHealth;
            ExperiencePoints = 0;
        }

        ~PlayerModel() { }

        public void SetController(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public int PlayerID { get; private set; }
        public PlayerView PlayerPrefab { get; private set; }
        public string CharacterName { get; private set; }
        public Vector3 SpawnPosition { get; private set; }
        public Vector3 SpawnRotation { get; private set; }
        public float MovementSpeed { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public int ExperiencePoints { get; private set; }

        public void UpdateCurrentHealth(int value)
        {
            CurrentHealth += value;
        }

        public void UpdateMaxHealth(int value)
        {
            MaxHealth += value;
        }

        public void UpdateExperiencePoints(int value)
        {
            ExperiencePoints += value;
        }
    }
}

