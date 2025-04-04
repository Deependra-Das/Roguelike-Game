using UnityEngine;
using UnityEngine.UI;
using Roguelike.Main;
using Roguelike.Event;

namespace Roguelike.UI
{
    public class PauseMenuUIView : MonoBehaviour, IUIView
    {
        private PauseMenuUIController _controller;
        [SerializeField] private Button _continueButtonPrefab;
        [SerializeField] private Button _giveUpButtonPrefab;

        public void SetController(IUIController controllerToSet)
        {
            _controller = controllerToSet as PauseMenuUIController;
        }

        private void OnEnable() => SubscribeToEvents();

        private void OnDisable() => UnsubscribeToEvents();

        private void SubscribeToEvents()
        {
            _continueButtonPrefab.onClick.AddListener(OnContinueButtonClicked);
            _giveUpButtonPrefab.onClick.AddListener(OnGiveUpButtonClicked);
        }

        private void UnsubscribeToEvents()
        {
            _continueButtonPrefab.onClick.RemoveListener(OnContinueButtonClicked);
            _giveUpButtonPrefab.onClick.RemoveListener(OnGiveUpButtonClicked);
        }

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

        private void OnContinueButtonClicked()
        {
            _controller.OnContinueButtonClicked();
        }

        private void OnGiveUpButtonClicked()
        {
            _controller.OnGiveUpButtonClicked();
        }
    }
}

