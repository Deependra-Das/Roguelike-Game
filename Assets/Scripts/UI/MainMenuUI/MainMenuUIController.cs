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
            Show();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnNewGameButtonClicked.AddListener(OnNewGameButtonClicked);
            GameService.Instance.GetService<EventService>().OnQuitGameButtonClicked.AddListener(OnQuitButtonClicked);
            GameService.Instance.GetService<EventService>().OnBackToMainMenuButtonClicked.AddListener(OnBackToMainMenuButtonClicked);            
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnNewGameButtonClicked.RemoveListener(OnNewGameButtonClicked);
            GameService.Instance.GetService<EventService>().OnQuitGameButtonClicked.RemoveListener(OnQuitButtonClicked);
            GameService.Instance.GetService<EventService>().OnBackToMainMenuButtonClicked.RemoveListener(OnBackToMainMenuButtonClicked);
        }

        public void Show()
        {
            _mainMenuUIView.EnableView();
        }

        public void Hide()
        {
            _mainMenuUIView.DisableView();
        }

        private void OnNewGameButtonClicked()
        {
            Hide();
        }

        private void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        private void OnBackToMainMenuButtonClicked()
        {
            Show();
        }

        private void OnDestroy()
        {
            _mainMenuUIView.OnDestroy();
            UnsubscribeToEvents();
        }
    }
}
