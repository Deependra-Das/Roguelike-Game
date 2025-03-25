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

        public GameOverUIController(GameOverUIView gameOverUIView)
        {
            _gameOverUIView = gameOverUIView;
            _gameOverUIView.SetController(this);
        }

        public void InitializeController()
        {
            _gameOverUIView.InitializeView();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnGameOver.AddListener(OnGameOver);
            GameService.Instance.GetService<EventService>().OnBackToMainMenuButtonClicked.AddListener(OnBackToMainMenu);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnGameOver.RemoveListener(OnGameOver);
            GameService.Instance.GetService<EventService>().OnBackToMainMenuButtonClicked.RemoveListener(OnBackToMainMenu);
        }

        public void Show()
        {
            _gameOverUIView.EnableView();
        }

        public void Hide()
        {
            _gameOverUIView.DisableView();
        }

        private void OnGameOver()
        {
            Show();
        }

        private void OnBackToMainMenu()
        {
            Hide();
        }

        private void OnDestroy()
        {
            _gameOverUIView.OnDestroy();
            UnsubscribeToEvents();
        }
    }

}