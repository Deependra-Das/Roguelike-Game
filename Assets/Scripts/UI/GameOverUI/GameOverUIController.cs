using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;

namespace Roguelike.UI
{
    public class GameOverUIController : IUIController
    {
        private GameOverUIView _gameOverUIView;
        private GameState _currentGameState;

        public GameOverUIController(GameOverUIView gameOverUIView)
        {
            _gameOverUIView = gameOverUIView;
            _gameOverUIView.SetController(this);
        }

        ~GameOverUIController() => UnsubscribeToEvents();

        public void InitializeController()
        {
            _gameOverUIView.InitializeView();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameOver.AddListener(Show);
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameOver.RemoveListener(Show);
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
        }

        public void Show()
        {
            _gameOverUIView.EnableView();
        }

        public void Hide()
        {
            _gameOverUIView.DisableView();
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void OnBackButtonClicked()
        {
            Hide();
            GameService.Instance.ChangeGameState(GameState.MainMenu);
        }

    }

}