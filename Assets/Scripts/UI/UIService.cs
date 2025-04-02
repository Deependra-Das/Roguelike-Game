using System.Collections.Generic;
using UnityEngine;
using Roguelike.Main;
using Roguelike.Player;
using Roguelike.Utilities;
using Roguelike.UI;
using Roguelike.Level;
using Roguelike.Event;

public class UIService : MonoBehaviour,IService
{
    [Header("Main Menu UI")]
    private MainMenuUIController _mainMenuUIController;
    [SerializeField] private MainMenuUIView _mainMenuUIView;

    [Header("Level Selection UI")]
    private LevelSelectionUIController _levelSelectionController;
    [SerializeField] private LevelSelectionUIView _levelSelectionView;
    [SerializeField] private LevelButtonView _levelButtonPrefab;
    [SerializeField] private List<LevelScriptableObject> _level_SO;

    [Header("Character Selection UI")]
    private CharacterSelectionUIController _characterSelectionController;
    [SerializeField] private CharacterSelectionUIView _characterSelectionView;
    [SerializeField] private CharacterButtonView _characterButtonPrefab;
    [SerializeField] private List<PlayerScriptableObject> _player_SO;

    [Header("Pause Menu UI")]
    private PauseMenuUIController _pauseMenuUIController;
    [SerializeField] private PauseMenuUIView _pauseMenuUIView;

    [Header("Game Over UI")]
    private GameOverUIController _gameOverUIController;
    [SerializeField] private GameOverUIView _gameOverUIView;

    [Header("Level Completed UI")]
    private LevelCompletedUIController _levelCompletedUIController;
    [SerializeField] private LevelCompletedUIView _levelCompletedUIView;

    [Header("Gameplay UI")]
    private GameplayUIController _gameplayUIController;
    [SerializeField] private GameplayUIView _gameplayUIView;

    [Header("PowerUp Selection UI")]
    private PowerUpSelectionUIController _powerUpSelectionUIController;
    [SerializeField] private PowerUpSelectionUIView _powerUpSelectionUIView;
    [SerializeField] private PowerUpButtonView _powerUpButtonView;
    [SerializeField] private HealthUpgradeButtonView _healthUpgradeButtonView;
    [SerializeField] private HealingButtonView _healingButtonView;

    private GameState _currentGameState;

    private void Awake()
    {
        _mainMenuUIController = new MainMenuUIController(_mainMenuUIView);
        _levelSelectionController = new LevelSelectionUIController(_levelSelectionView, _levelButtonPrefab, _level_SO);
        _characterSelectionController = new CharacterSelectionUIController(_characterSelectionView, _characterButtonPrefab, _player_SO);
        _pauseMenuUIController = new PauseMenuUIController(_pauseMenuUIView);
        _gameOverUIController = new GameOverUIController(_gameOverUIView);
        _levelCompletedUIController = new LevelCompletedUIController(_levelCompletedUIView);
        _gameplayUIController = new GameplayUIController(_gameplayUIView);
        _powerUpSelectionUIController = new PowerUpSelectionUIController(_powerUpSelectionUIView, _powerUpButtonView, _healthUpgradeButtonView, _healingButtonView);
    }

    public void Initialize(params object[] dependencies)
    {
        _mainMenuUIController.InitializeController();
        _levelSelectionController.InitializeController();
        _characterSelectionController.InitializeController();
        _pauseMenuUIController.InitializeController();
        _gameOverUIController.InitializeController();
        _levelCompletedUIController.InitializeController();
        _gameplayUIController.InitializeController();
        _powerUpSelectionUIController.InitializeController();
        SubscribeToEvents();
    }

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

    private void OnDestroy() => UnsubscribeToEvents();
}
