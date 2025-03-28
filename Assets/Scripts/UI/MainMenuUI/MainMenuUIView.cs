using UnityEngine;
using UnityEngine.UI;
using Roguelike.Main;
using Roguelike.Event;

namespace Roguelike.UI
{
    public class MainMenuUIView : MonoBehaviour, IUIView
    {
        private MainMenuUIController controller;
        [SerializeField] private Button _newGameButtonPrefab;
        [SerializeField] private Button _quitGameButtonPrefab;

        public void SetController(IUIController controllerToSet)
        {
            controller = controllerToSet as MainMenuUIController;
        }

        public void InitializeView()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _newGameButtonPrefab.onClick.AddListener(OnNewGameButtonClicked);
        }

        private void UnsubscribeToEvents()
        {
            _newGameButtonPrefab.onClick.RemoveListener(OnNewGameButtonClicked);
        }

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

        public void OnDestroy() => UnsubscribeToEvents();

        private void OnNewGameButtonClicked()
        {
            GameService.Instance.GetService<EventService>().OnNewGameButtonClicked.Invoke();
        }
    }
}
