using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;

namespace Roguelike.UI
{
    public class PauseMenuUIController : IUIController
    {
        private PauseMenuUIView _pauseMenuUIView;

        public PauseMenuUIController(PauseMenuUIView pauseMenuUIView)
        {
            _pauseMenuUIView = pauseMenuUIView;
            _pauseMenuUIView.SetController(this);
        }

        public void InitializeController()
        {
            _pauseMenuUIView.InitializeView();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnPauseGame.AddListener(OnPauseGame);
            GameService.Instance.GetService<EventService>().OnContinueButtonClicked.AddListener(OnContinueGame);
            GameService.Instance.GetService<EventService>().OnGiveUpButtonClicked.AddListener(OnGiveUp);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnPauseGame.RemoveListener(OnPauseGame);
            GameService.Instance.GetService<EventService>().OnContinueButtonClicked.RemoveListener(OnContinueGame);
            GameService.Instance.GetService<EventService>().OnGiveUpButtonClicked.RemoveListener(OnGiveUp);
        }

        public void Show()
        {
            _pauseMenuUIView.EnableView();
        }

        public void Hide()
        {
            _pauseMenuUIView.DisableView();
        }

        private void OnPauseGame()
        {
            Show();
        }

        private void OnContinueGame()
        {
            Hide();
        }

        private void OnGiveUp()
        {
            Hide();
            GameService.Instance.GetService<EventService>().OnGameOver.Invoke();
        }

        private void OnDestroy()
        {
            _pauseMenuUIView.OnDestroy();
            UnsubscribeToEvents();
        }
    }
}
