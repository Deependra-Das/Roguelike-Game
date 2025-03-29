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

        public MainMenuUIController(MainMenuUIView mainMenuUIView)
        {
            _mainMenuUIView = mainMenuUIView;
            _mainMenuUIView.SetController(this);
        }

        public void InitializeController()
        {
            _mainMenuUIView.InitializeView();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnMainMenu.AddListener(Show);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnMainMenu.RemoveListener(Show);
        }

        public void Show()
        {
            _mainMenuUIView.EnableView();
        }

        public void Hide()
        {
            _mainMenuUIView.DisableView();
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

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
