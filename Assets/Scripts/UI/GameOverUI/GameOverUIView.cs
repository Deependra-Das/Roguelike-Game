using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;

namespace Roguelike.UI
{
    public class GameOverUIView : MonoBehaviour, IUIView
    {
        private GameOverUIController controller;
        [SerializeField] private Button _backToMainMenuButtonPrefab;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as GameOverUIController;
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
            GameService.Instance.GetService<EventService>().OnBackToMainMenuButtonClicked.Invoke();
        }
    }
}