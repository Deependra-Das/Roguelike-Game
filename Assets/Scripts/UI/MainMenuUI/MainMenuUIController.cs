using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Sound;

namespace Roguelike.UI
{
    public class MainMenuUIController : IUIController
    {
        private MainMenuUIView _mainMenuUIView;
        private GameState _currentGameState;

        public MainMenuUIController(MainMenuUIView mainMenuUIView)
        {
            _mainMenuUIView = mainMenuUIView;
            _mainMenuUIView.SetController(this);
        }

        ~MainMenuUIController() => UnsubscribeToEvents();

        public void InitializeController()
        {
            _mainMenuUIView.InitializeView();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnMainMenu.AddListener(Show);
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnMainMenu.RemoveListener(Show);
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
        }

        public void Show()
        {
            GameService.Instance.GetService<SoundService>().PlayBGM(SoundType.MainBGM, true);
            _mainMenuUIView.EnableView();
        }

        public void Hide()
        {
            _mainMenuUIView.DisableView();
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void OnNewGameButtonClicked()
        {
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.ButtonClick);
            Hide();
            GameService.Instance.ChangeGameState(GameState.LevelSelection);
        }

        public void OnQuitButtonClicked()
        {
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.ButtonClick);
            Application.Quit();
        }
    }
}
