using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;

namespace Roguelike.UI
{
    public class LevelCompletedUIController : IUIController
    {
        private LevelCompletedUIView _levelCompletedUIView;

        public LevelCompletedUIController(LevelCompletedUIView levelCompletedUIView)
        {
            _levelCompletedUIView = levelCompletedUIView;
            _levelCompletedUIView.SetController(this);
        }

        public void InitializeController()
        {
            _levelCompletedUIView.InitializeView();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnLevelCompleted.AddListener(OnLevelCompleted);
            GameService.Instance.GetService<EventService>().OnBackToMainMenuButtonClicked.AddListener(OnBackToMainMenu);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnLevelCompleted.RemoveListener(OnLevelCompleted);
            GameService.Instance.GetService<EventService>().OnBackToMainMenuButtonClicked.RemoveListener(OnBackToMainMenu);
        }

        public void Show()
        {
            _levelCompletedUIView.EnableView();
        }

        public void Hide()
        {
            _levelCompletedUIView.DisableView();
        }

        private void OnLevelCompleted()
        {
            Show();
        }

        private void OnBackToMainMenu()
        {
            Hide();
        }

        private void OnDestroy()
        {
            _levelCompletedUIView.OnDestroy();
            UnsubscribeToEvents();
        }
    }

}