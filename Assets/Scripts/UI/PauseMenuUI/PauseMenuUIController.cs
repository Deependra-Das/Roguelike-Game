using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Sound;

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
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.GamePause);
            _pauseMenuUIView.EnableView();
            Time.timeScale = 0;
        }

        public void Hide()
        {
            _pauseMenuUIView.DisableView();
            Time.timeScale = 1f;
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void OnContinueButtonClicked()
        {
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.ButtonClick);
            Hide();
            GameService.Instance.ChangeGameState(GameState.Gameplay);
        }

        public void OnGiveUpButtonClicked()
        {
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.ButtonClick);
            Hide();
            GameService.Instance.ChangeGameState(GameState.GameOver);
        }

    }
}
