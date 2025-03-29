using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;

namespace Roguelike.UI
{
    public class MainMenuUIController : IUIController
    {
        private MainMenuUIView _mainMenuUIView;
        public GameState CurrentGameState { get; private set; }

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
            _mainMenuUIView.EnableView();
        }

        public void Hide()
        {
            _mainMenuUIView.DisableView();
        }

        public void SetGameState(GameState _newState)
        {
            CurrentGameState = _newState;
        }

        public void OnNewGameButtonClicked()
        {
            Hide();
            GameService.Instance.ChangeGameState(GameState.LevelSelection);
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }
    }
}
