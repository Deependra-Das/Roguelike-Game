using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;

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
        }

        public void Show()
        {
            _gameplayUIView.EnableView();
        }

        public void Hide()
        {
            _gameplayUIView.DisableView();
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
