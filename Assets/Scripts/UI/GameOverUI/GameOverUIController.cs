using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Sound;

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

        ~GameOverUIController() => UnsubscribeToEvents();

        public void InitializeController()
        {
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameOver.AddListener(Show);
            EventService.Instance.OnSetFinalScore.AddListener(UpdateScoreData);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameOver.RemoveListener(Show);
            EventService.Instance.OnSetFinalScore.RemoveListener(UpdateScoreData);
        }

        public void Show()
        {
            GameService.Instance.GetService<SoundService>().PlayBGM(SoundType.GameOver);
            _gameOverUIView.EnableView();
        }

        public void Hide()
        {
            _gameOverUIView.DisableView();
        }

        private void UpdateScoreData(float score, float highScore)
        {
            _gameOverUIView.UpdateScoreData(score, highScore);
        }

        public void OnBackButtonClicked()
        {
            Hide();
            GameService.Instance.ChangeGameState(GameState.MainMenu);
        }

    }

}