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
            GameService.Instance.GetService<EventService>().OnNewGameButtonSelected.AddListener(OnNewGameButtonClicked);
            GameService.Instance.GetService<EventService>().OnQuitGameButtonSelected.AddListener(OnQuitButtonClicked);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnNewGameButtonSelected.RemoveListener(OnNewGameButtonClicked);
            GameService.Instance.GetService<EventService>().OnQuitGameButtonSelected.RemoveListener(OnQuitButtonClicked);
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

        private void OnDestroy()
        {
            _mainMenuUIView.OnDestroy();
            UnsubscribeToEvents();
        }
    }
}
