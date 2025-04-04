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

        public LevelCompletedUIController(LevelCompletedUIView levelCompletedUIView)
        {
            _levelCompletedUIView = levelCompletedUIView;
            _levelCompletedUIView.SetController(this);
        }

        ~LevelCompletedUIController() => UnsubscribeToEvents();

        public void InitializeController()
        {
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnLevelCompleted.AddListener(Show);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnLevelCompleted.RemoveListener(Show);
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

        public void OnBackButtonClicked()
        {
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.ButtonClick);
            Hide();
            GameService.Instance.ChangeGameState(GameState.MainMenu);
        }

    }

}