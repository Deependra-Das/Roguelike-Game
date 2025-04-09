using System.Collections.Generic;
using UnityEngine;
using Roguelike.Main;
using Roguelike.Player;
using Roguelike.Utilities;
using Roguelike.Level;
using Roguelike.Event;

namespace Roguelike.UI
{
    public class UIService : IService
    { 
        private GameState _currentGameState;

        private Transform _uiCanvasTransform;
        private Transform _dmgNumcanvasTransform;

        private LevelScriptableObject _level_SO;
        private PlayerScriptableObject _player_SO;
        private UI_Data_ScriptableObject _uiData_SO;

        private MainMenuUIController _mainMenuUIController;
        private LevelSelectionUIController _levelSelectionController;
        private CharacterSelectionUIController _characterSelectionController;
        private PauseMenuUIController _pauseMenuUIController;
        private GameOverUIController _gameOverUIController;
        private LevelCompletedUIController _levelCompletedUIController;
        private GameplayUIController _gameplayUIController;
        private PowerUpSelectionUIController _powerUpSelectionUIController;

        public UIService(UI_Data_ScriptableObject uI_Data_SO, LevelScriptableObject level_SO, PlayerScriptableObject player_SO)
        {
            _uiData_SO = uI_Data_SO;
            _level_SO = level_SO;
            _player_SO = player_SO;
            CreateCanvas();
            SubscribeToEvents();
        }

        ~UIService() => UnsubscribeToEvents();

        public void Initialize(params object[] dependencies)
        {
            IntializeControllers();
        }

        private void CreateCanvas()
        {
            GameObject uiCanvas = Object.Instantiate(_uiData_SO.uiCanvas);
            _uiCanvasTransform = uiCanvas.transform;

            GameObject dmgNumCanvas = Object.Instantiate(_uiData_SO.dmgNumCanvas);
            _dmgNumcanvasTransform = dmgNumCanvas.transform;
        }

        private void IntializeControllers()
        {
            _mainMenuUIController = new MainMenuUIController(_uiData_SO.mainMenuUIPrefab, _uiCanvasTransform);
            _levelSelectionController = new LevelSelectionUIController(_uiData_SO.levelSelectionPrefab, _uiData_SO.levelButtonPrefab, _uiCanvasTransform, _level_SO);
            _characterSelectionController = new CharacterSelectionUIController(_uiData_SO.characterSelectionPrefab, _uiData_SO.characterButtonPrefab, _uiCanvasTransform, _player_SO);
            _gameplayUIController = new GameplayUIController(_uiData_SO.gameplayUIPrefab, _uiCanvasTransform);
            _powerUpSelectionUIController = new PowerUpSelectionUIController(_uiData_SO.powerUpSelectionUIPrefab, _uiData_SO.powerUpButtonPrefab, _uiData_SO.healthUpgradeButtonPrefab, _uiData_SO.healingButtonPrefab, _uiCanvasTransform);
            _pauseMenuUIController = new PauseMenuUIController(_uiData_SO.pauseMenuUIPrefab, _uiCanvasTransform);
            _gameOverUIController = new GameOverUIController(_uiData_SO.gameOverUIPrefab, _uiCanvasTransform);
            _levelCompletedUIController = new LevelCompletedUIController(_uiData_SO.levelCompletedUIPrefab, _uiCanvasTransform);       
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
        }

        public Transform GetCanvasTransform => _uiCanvasTransform;

        public Transform GetDamageCanvasTransform => _dmgNumcanvasTransform;
    }
}

