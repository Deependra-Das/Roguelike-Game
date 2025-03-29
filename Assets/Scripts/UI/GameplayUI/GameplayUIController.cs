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

        public GameplayUIController(GameplayUIView gameplayUIView)
        {
            _gameplayUIView = gameplayUIView;
            _gameplayUIView.SetController(this);
        }

        public void InitializeController()
        {
            _gameplayUIView.InitializeView();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnStartGame.AddListener(Show);
            GameService.Instance.GetService<EventService>().OnGameOver.AddListener(Hide);
            GameService.Instance.GetService<EventService>().OnLevelCompleted.AddListener(Hide);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnStartGame.RemoveListener(Show);
            GameService.Instance.GetService<EventService>().OnGameOver.RemoveListener(Hide);
            GameService.Instance.GetService<EventService>().OnLevelCompleted.RemoveListener(Hide);
            GameService.Instance.GetService<EventService>().OnLevelCompleted.AddListener(Hide);
        }

        public void Show()
        {
            _gameplayUIView.ResetTimer();
            _gameplayUIView.EnableView();
            _gameplayUIView.OnGamePause(false);
            _gameplayUIView.OnGameOver(false);
        }

        public void Hide()
        {
            _gameplayUIView.DisableView();
            _gameplayUIView.OnGameOver(false);
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }

        private void OnGamePause()
        {
            _gameplayUIView.OnGamePause(true);
        }

        private void OnGameContinue()
        {
            _gameplayUIView.OnGamePause(false);
        }

        private void OnGameOver()
        {
            _gameplayUIView.OnGameOver(true);
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
