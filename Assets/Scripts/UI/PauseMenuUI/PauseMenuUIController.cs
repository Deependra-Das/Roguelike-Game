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
        private GameState _currentGameState;

        public PauseMenuUIController(PauseMenuUIView pauseMenuUIView)
        {
            _pauseMenuUIView = pauseMenuUIView;
            _pauseMenuUIView.SetController(this);
        }

        ~PauseMenuUIController() => UnsubscribeToEvents();

        public void InitializeController()
        {
            _pauseMenuUIView.InitializeView();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
            EventService.Instance.OnGamePaused.AddListener(Show);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnGamePaused.RemoveListener(Show);
        }

        public void Show()
        {
            _pauseMenuUIView.EnableView();
        }

        public void Hide()
        {
            _pauseMenuUIView.DisableView();
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void OnContinueButtonClicked()
        {
            Hide();
            GameService.Instance.ChangeGameState(GameState.Gameplay);
        }

        public void OnGiveUpButtonClicked()
        {
            Hide();
            GameService.Instance.ChangeGameState(GameState.GameOver);
        }

    }
}
