using System.Collections.Generic;
using UnityEngine;
using Roguelike.Main;
using Roguelike.Player;
using Roguelike.Utilities;
using Roguelike.Level;
using Roguelike.Event;

namespace Roguelike.UI
{
    public class UIService : GenericMonoSingleton<UIService>
    {
        #region Inspector Dependencies

        [Header("Canvas Transforms")]
        [SerializeField] private Transform _canvasTransform;
        [SerializeField] private Transform _dmgCanvasTransform;

        [Header("Main Menu UI")]
        [SerializeField] private MainMenuUIView _mainMenuUIView;

        [Header("Level Selection UI")]
        [SerializeField] private LevelSelectionUIView _levelSelectionView;
        [SerializeField] private LevelButtonView _levelButtonPrefab;

        [Header("Character Selection UI")]
        [SerializeField] private CharacterSelectionUIView _characterSelectionView;
        [SerializeField] private CharacterButtonView _characterButtonPrefab;

        [Header("Pause Menu UI")]
        [SerializeField] private PauseMenuUIView _pauseMenuUIView;

        [Header("Game Over UI")]
        [SerializeField] private GameOverUIView _gameOverUIView;

        [Header("Level Completed UI")]
        [SerializeField] private LevelCompletedUIView _levelCompletedUIView;

        [Header("Gameplay UI")]
        [SerializeField] private GameplayUIView _gameplayUIView;

        [Header("PowerUp Selection UI")]
        [SerializeField] private PowerUpSelectionUIView _powerUpSelectionUIView;
        [SerializeField] private PowerUpButtonView _powerUpButtonView;
        [SerializeField] private HealthUpgradeButtonView _healthUpgradeButtonView;
        [SerializeField] private HealingButtonView _healingButtonView;

        #endregion

        private GameState _currentGameState;
        private List<LevelScriptableObject> _level_SO;
        private List<PlayerScriptableObject> _player_SO;

        private MainMenuUIController _mainMenuUIController;
        private LevelSelectionUIController _levelSelectionController;
        private CharacterSelectionUIController _characterSelectionController;
        private PauseMenuUIController _pauseMenuUIController;
        private GameOverUIController _gameOverUIController;
        private LevelCompletedUIController _levelCompletedUIController;
        private GameplayUIController _gameplayUIController;
        private PowerUpSelectionUIController _powerUpSelectionUIController;

        protected override void Awake()
        {
            base.Awake();
        }

        public void Initialize(params object[] dependencies)
        {
            _level_SO = (List<LevelScriptableObject>)dependencies[0];
            _player_SO = (List<PlayerScriptableObject>)dependencies[1];

            _mainMenuUIController = new MainMenuUIController(_mainMenuUIView);
            _levelSelectionController = new LevelSelectionUIController(_levelSelectionView, _levelButtonPrefab, _level_SO);
            _characterSelectionController = new CharacterSelectionUIController(_characterSelectionView, _characterButtonPrefab, _player_SO);
            _pauseMenuUIController = new PauseMenuUIController(_pauseMenuUIView);
            _gameOverUIController = new GameOverUIController(_gameOverUIView);
            _levelCompletedUIController = new LevelCompletedUIController(_levelCompletedUIView);
            _gameplayUIController = new GameplayUIController(_gameplayUIView);
            _powerUpSelectionUIController = new PowerUpSelectionUIController(_powerUpSelectionUIView, _powerUpButtonView, _healthUpgradeButtonView, _healingButtonView);
            SubscribeToEvents();
        }

        private void OnDisable() => UnsubscribeToEvents();

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
        }

        public void UpdateCurrentHealthSlider(float currentHealth)
        {
            _gameplayUIController.UpdateCurrentHealthSlider(currentHealth);
        }

        public void UpdateMaxHealthSlider(float maxHealth)
        {
            _gameplayUIController.UpdateMaxHealthSlider(maxHealth);
        }

        public void UpdateCurrentExpSlider(float currentExp)
        {
            _gameplayUIController.UpdateCurrentExpSlider(currentExp);
        }

        public void UpdateMaxExpSlider(float maxExp)
        {
            _gameplayUIController.UpdateMaxExpSlider(maxExp);
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
            _gameplayUIView.SetGameState(_currentGameState);
        }

        public Transform GetCanvasTransform => _canvasTransform;

        public Transform GetDamageCanvasTransform => _dmgCanvasTransform;
    }
}

