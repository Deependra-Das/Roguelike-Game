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

        public LevelCompletedUIController(LevelCompletedUIView levelCompletedUIPrefab, Transform uiCanvasTransform)
        {
            _levelCompletedUIView = Object.Instantiate(levelCompletedUIPrefab, uiCanvasTransform);
            _levelCompletedUIView.SetController(this);
            SubscribeToEvents();
            Hide();
        }

        ~LevelCompletedUIController() => UnsubscribeToEvents();

        private void SubscribeToEvents()
        {
            EventService.Instance.OnLevelCompleted.AddListener(Show);
            EventService.Instance.OnSetFinalScore.AddListener(UpdateScoreData);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnLevelCompleted.RemoveListener(Show);
            EventService.Instance.OnSetFinalScore.RemoveListener(UpdateScoreData);
        }

        public void Show()
        {
            ServiceLocator.Instance.GetService<SoundService>().PlayBGM(SoundType.LevelCompleted);
            _levelCompletedUIView.EnableView();
        }

        public void Hide()
        {
            _levelCompletedUIView.DisableView();
        }

        private void UpdateScoreData(float score, float highScore)
        {
            _levelCompletedUIView.UpdateScoreData(score, highScore);
        }

        public void OnBackButtonClicked()
        {
            ServiceLocator.Instance.GetService<SoundService>().PlaySFX(SoundType.ButtonClick);
            Hide();
            GameService.Instance.ChangeGameState(GameState.MainMenu);
        }

    }

}