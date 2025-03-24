using Roguelike.Event;
using Roguelike.Main;
using UnityEngine;

namespace Roguelike.UI
{
    public class LevelSelectionUIController : IUIController
    {
        private LevelSelectionUIView _levelSelectionUIView;

        public LevelSelectionUIController(LevelSelectionUIView levelSelectionUIView)
        {
            _levelSelectionUIView = levelSelectionUIView;
            _levelSelectionUIView.SetController(this);
        }

        public void InitializeController()
        {
            _levelSelectionUIView.InitializeView();
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
            _levelSelectionUIView.EnableView();
        }

        public void Hide()
        {
            _levelSelectionUIView.DisableView();
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
            _levelSelectionUIView.OnDestroy();
        }
    }
}
