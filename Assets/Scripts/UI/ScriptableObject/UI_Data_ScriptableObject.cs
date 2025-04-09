using Roguelike.UI;
using UnityEngine;

namespace Roguelike.UI
{
    [CreateAssetMenu(fileName = "UI_Data_ScriptableObject", menuName = "ScriptableObjects/UI_Data_ScriptableObject")]
    public class UI_Data_ScriptableObject : ScriptableObject
    {
        [Header("Canvas")]
        public GameObject uiCanvas;
        public GameObject dmgNumCanvas;

        [Header("Main Menu UI")]
        public MainMenuUIView mainMenuUIPrefab;

        [Header("Level Selection UI")]
        public LevelSelectionUIView levelSelectionPrefab;
        public LevelButtonView levelButtonPrefab;

        [Header("Character Selection UI")]
        public CharacterSelectionUIView characterSelectionPrefab;
        public CharacterButtonView characterButtonPrefab;

        [Header("Pause Menu UI")]
        public PauseMenuUIView pauseMenuUIPrefab;

        [Header("Game Over UI")]
        public GameOverUIView gameOverUIPrefab;

        [Header("Level Completed UI")]
        public LevelCompletedUIView levelCompletedUIPrefab;

        [Header("Gameplay UI")]
        public GameplayUIView gameplayUIPrefab;

        [Header("PowerUp Selection UI")]
        public PowerUpSelectionUIView powerUpSelectionUIPrefab;
        public PowerUpButtonView powerUpButtonPrefab;
        public HealthUpgradeButtonView healthUpgradeButtonPrefab;
        public HealingButtonView healingButtonPrefab;
    }
}