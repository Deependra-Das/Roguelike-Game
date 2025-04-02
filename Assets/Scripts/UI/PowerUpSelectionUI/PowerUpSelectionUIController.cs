using UnityEngine;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Weapon;
using Roguelike.Projectile;
using Unity.VisualScripting;

namespace Roguelike.UI
{
    public class PowerUpSelectionUIController : IUIController
    {
        private PowerUpSelectionUIView _powerUpSelectionUIView;
        private PowerUpButtonView _powerUpButtonPrefab;
        private List<PowerUpButtonView> _powerupButtonList;
        private GameState _currentGameState;

        public PowerUpSelectionUIController(PowerUpSelectionUIView powerUpSelectionUIView, PowerUpButtonView powerUpButtonPrefab)
        {
            _powerUpButtonPrefab = powerUpButtonPrefab;
            _powerUpSelectionUIView = powerUpSelectionUIView;
            _powerUpSelectionUIView.SetController(this);
        }

        ~PowerUpSelectionUIController() => UnsubscribeToEvents();

        public void InitializeController()
        {
            _powerupButtonList = new List<PowerUpButtonView>();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
            EventService.Instance.OnPowerUpSelection.AddListener(Show);
            EventService.Instance.OnWeaponAdded.AddListener(CreatePowerUpButtons);
            EventService.Instance.OnGameOver.AddListener(OnGameOver);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnPowerUpSelection.RemoveListener(Show);
            EventService.Instance.OnWeaponAdded.RemoveListener(CreatePowerUpButtons);
            EventService.Instance.OnGameOver.RemoveListener(OnGameOver);
        }


        public void Show()
        {
            _powerUpSelectionUIView.EnableView();
            UpdatePowerUpButtons();
            Time.timeScale = 0;
        }

        public void Hide()
        {
            Time.timeScale = 1;
            _powerUpSelectionUIView.DisableView();
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void CreatePowerUpButtons(List<WeaponController> weaponList)
        {
            _powerupButtonList.Clear();
            
            foreach (WeaponController weapon in weaponList)
            {
                var newButton = _powerUpSelectionUIView.AddButton(_powerUpButtonPrefab);
                newButton.SetOwner(this);        
                newButton.InitializeView(weapon);
                _powerupButtonList.Add(newButton);
            }
        }

        public void UpdatePowerUpButtons()
        {
            foreach (var button in _powerupButtonList)
            {
                button.SetPowerUpButtonData();
            }
        }

        public void OnPowerUpSelected(WeaponController weaponObj)
        {
            weaponObj.ActivateUpgradeWeapon();
            UpdatePowerUpButtons();
            Hide();
            GameService.Instance.ChangeGameState(GameState.Gameplay);
        }

        private void OnGameOver()
        {
            UnsubscribeToEvents();
            foreach (var button in _powerupButtonList)
            {
                button.OnDestroy();
            }            
        }
    }
}
