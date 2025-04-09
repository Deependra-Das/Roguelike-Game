using UnityEngine;
using UnityEngine.UI;
using Roguelike.Main;
using Roguelike.Event;
using Roguelike.Sound;

namespace Roguelike.UI
{
    public class MainMenuUIView : MonoBehaviour
    {
        [SerializeField] private Button _newGameButtonPrefab;
        [SerializeField] private Button _quitGameButtonPrefab;
        private MainMenuUIController _controller;

        public void SetController(IUIController controllerToSet)
        {
            _controller = controllerToSet as MainMenuUIController;
        }

        private void OnEnable() => SubscribeToEvents();

        private void OnDisable() => UnsubscribeToEvents();

        private void SubscribeToEvents()
        {
            _newGameButtonPrefab.onClick.AddListener(OnNewGameButtonClicked);
        }

        private void UnsubscribeToEvents()
        {
            _newGameButtonPrefab.onClick.RemoveListener(OnNewGameButtonClicked);
        }

        private void OnNewGameButtonClicked()
        {
            _controller.OnNewGameButtonClicked();
        }

        private void OnQuitGameButtonClicked()
        {
            _controller.OnQuitButtonClicked();
        }

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

    }
}
