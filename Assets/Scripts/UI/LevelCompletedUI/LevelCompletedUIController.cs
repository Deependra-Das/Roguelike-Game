using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Sound;

namespace Roguelike.UI
{
    public class LevelCompletedUIController : IUIController
    {
        private LevelCompletedUIView _levelCompletedUIView;
        private GameState _currentGameState;

        public LevelCompletedUIController(LevelCompletedUIView levelCompletedUIView)
        {
            _levelCompletedUIView = levelCompletedUIView;
            _levelCompletedUIView.SetController(this);
        }
        ~LevelCompletedUIController() => UnsubscribeToEvents();

        public void InitializeController()
        {
            _levelCompletedUIView.InitializeView();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnLevelCompleted.AddListener(Show);
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnLevelCompleted.RemoveListener(Show);
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
        }

        public void Show()
        {
            GameService.Instance.GetService<SoundService>().PlayBGM(SoundType.LevelCompleted);
            _levelCompletedUIView.EnableView();
        }

        public void Hide()
        {
            _levelCompletedUIView.DisableView();
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void OnBackButtonClicked()
        {
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.ButtonClick);
            Hide();
            GameService.Instance.ChangeGameState(GameState.MainMenu);
        }

        private void OnDestroy()
        {
            _levelCompletedUIView.OnDestroy();
            UnsubscribeToEvents();
        }
    }

}