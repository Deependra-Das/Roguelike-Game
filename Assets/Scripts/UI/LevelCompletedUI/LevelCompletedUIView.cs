using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;

namespace Roguelike.UI
{
    public class LevelCompletedUIView : MonoBehaviour, IUIView
    {
        private LevelCompletedUIController _controller;
        [SerializeField] private Button _backToMainMenuButtonPrefab;

        public void SetController(IUIController controllerToSet)
        {
            _controller = controllerToSet as LevelCompletedUIController;
        }

        public void InitializeView()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _backToMainMenuButtonPrefab.onClick.AddListener(OnBackToMainMenuButtonClicked);
        }

        private void UnsubscribeToEvents()
        {
            _backToMainMenuButtonPrefab.onClick.RemoveListener(OnBackToMainMenuButtonClicked);
        }

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

        public void OnDestroy() => UnsubscribeToEvents();

        private void OnBackToMainMenuButtonClicked()
        {
            _controller.OnBackButtonClicked();
        }
    }
}