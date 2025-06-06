using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Player;

namespace Roguelike.UI
{
    public class GameplayUIController : IUIController
    {
        private GameplayUIView _gameplayUIView;
        private GameState _currentGameState;

        public GameplayUIController(GameplayUIView gameplayUIPrefab, Transform uiCanvasTransform)
        {
            _gameplayUIView = Object.Instantiate(gameplayUIPrefab, uiCanvasTransform);
            _gameplayUIView.SetController(this);
            SubscribeToEvents();
        }

        ~GameplayUIController()
        {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnStartGameplay.AddListener(Show);
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
            EventService.Instance.OnGameOver.AddListener(Hide);
            EventService.Instance.OnLevelCompleted.AddListener(Hide);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnStartGameplay.RemoveListener(Show);
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnGameOver.RemoveListener(Hide);
            EventService.Instance.OnLevelCompleted.RemoveListener(Hide);
        }

        public void Show()
        {
            _gameplayUIView.EnableView();
        }

        public void Hide()
        {
            _gameplayUIView.DisableView();
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
            _gameplayUIView.SetGameState(_currentGameState);
        }

        public void UpdateCurrentHealthSlider(float currentHealth)
        {
            _gameplayUIView.UpdateCurrentHealthSlider(currentHealth);
        }

        public void UpdateMaxHealthSlider(float maxHealth)
        {
            _gameplayUIView.UpdateMaxHealthSlider(maxHealth);
        }

        public void UpdateCurrentExpSlider(float currentExp)
        {
            _gameplayUIView.UpdateCurrentExpSlider(currentExp);
        }

        public void UpdateMaxExpSlider(float maxExp)
        {
            _gameplayUIView.UpdateMaxExpSlider(maxExp);
        }
    }
}
