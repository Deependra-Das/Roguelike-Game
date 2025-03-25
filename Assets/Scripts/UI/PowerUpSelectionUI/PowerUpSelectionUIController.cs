using UnityEngine;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Player;

namespace Roguelike.UI
{
    public class PowerUpSelectionUIController : IUIController
    {
        private PowerUpSelectionUIView _powerUpSelectionUIView;
        [SerializeField] private PowerUpButtonView _powerUpButtonPrefab;
        [SerializeField] private List<PlayerScriptableObject> _powerUp_SO;

        public PowerUpSelectionUIController(PowerUpSelectionUIView powerUpSelectionUIView, PowerUpButtonView powerUpButtonPrefab, List<PlayerScriptableObject> powerUp_SO)
        {
            _powerUp_SO = powerUp_SO;
            _powerUpButtonPrefab = powerUpButtonPrefab;
            _powerUpSelectionUIView = powerUpSelectionUIView;
            _powerUpSelectionUIView.SetController(this);
        }

        public void InitializeController()
        {
            CreatePowerUpButtons();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnLevelSelected.AddListener(ShowPowerUpSelectionUI);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnLevelSelected.RemoveListener(ShowPowerUpSelectionUI);
        }

        public void ShowPowerUpSelectionUI(int levelId) => Show();

        public void Show()
        {
            _powerUpSelectionUIView.EnableView();
        }

        public void Hide()
        {
            _powerUpSelectionUIView.DisableView();
        }

        public void CreatePowerUpButtons()
        {
            foreach (var powerUpData in _powerUp_SO)
            {
                var newButton = _powerUpSelectionUIView.AddButton(_powerUpButtonPrefab);
                newButton.SetOwner(this);
                newButton.SetPowerUpButtonData();
            }
        }

        public void OnPowerUpSelected(int powerUpId)
        {
            Hide();
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
