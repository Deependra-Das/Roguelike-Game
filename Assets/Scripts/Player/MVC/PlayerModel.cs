using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerModel
    {
        private PlayerController _playerController;

        public PlayerModel(PlayerData playerDataObj)
        {
            PlayerID = playerDataObj.ID;
            PlayerPrefab = playerDataObj.playerPrefab;
            PlayerImage = playerDataObj.playerImage;
            CharacterName = playerDataObj.characterName;
            SpawnPosition = playerDataObj.spawnPosition;
            SpawnRotation = playerDataObj.spawnRotation;
            MovementSpeed = playerDataObj.movementSpeed;
            MaxHealth = playerDataObj.maxHealth;
            CurrentHealth = playerDataObj.maxHealth;
            ImmunityDuration = playerDataObj.immunityDuration;
            CurrentExpPoints = 0;
            CurrentExpLevel = 0;
        }

        ~PlayerModel() { }

        public void SetController(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public int PlayerID { get; private set; }
        public PlayerView PlayerPrefab { get; private set; }
        public Sprite PlayerImage { get; private set; }
        public string CharacterName { get; private set; }
        public Vector3 SpawnPosition { get; private set; }
        public Vector3 SpawnRotation { get; private set; }
        public float MovementSpeed { get; private set; }
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public int CurrentExpPoints { get; private set; }
        public int CurrentExpLevel { get; private set; }
        public float ImmunityDuration { get; private set; }

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
            CurrentExpPoints += value;
        }

        public void UpdateExpLevel()
        {
            CurrentExpLevel++;
        }
    }
}

