using UnityEngine;
using UnityEngine.UI;
using Roguelike.Main;
using Roguelike.Event;

namespace Roguelike.UI
{
    public class PauseMenuUIView : MonoBehaviour, IUIView
    {
        private PauseMenuUIController controller;
        [SerializeField] private Button _continueButtonPrefab;
        [SerializeField] private Button _giveUpButtonPrefab;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as PauseMenuUIController;
        }

        public void InitializeView()
        {
            SubscribeToEvents();
        }

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

        public void OnDestroy() => UnsubscribeToEvents();

        private void OnContinueButtonClicked()
        {
            GameService.Instance.GetService<EventService>().OnContinueButtonClicked.Invoke();
        }

        private void OnGiveUpButtonClicked()
        {
            GameService.Instance.GetService<EventService>().OnGiveUpButtonClicked.Invoke();
        }
    }
}

