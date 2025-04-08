using UnityEngine;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Weapon;
using Roguelike.Player;
using Roguelike.Projectile;
using Roguelike.Sound;

namespace Roguelike.UI
{
    public class PowerUpSelectionUIController : IUIController
    {
        private PowerUpSelectionUIView _powerUpSelectionUIView;
        private PowerUpButtonView _powerUpButtonPrefab;
        private HealthUpgradeButtonView _healthUpgradeButtonPrefab;
        private HealingButtonView _healingButtonPrefab;
        private List<PowerUpButtonView> _weaponPowerupButtonList;
        private HealthUpgradeButtonView _healthUpgradeButtonView;
        private HealingButtonView _healingButtonView;

        private GameState _currentGameState;

        public PowerUpSelectionUIController(PowerUpSelectionUIView powerUpSelectionUIView, PowerUpButtonView powerUpButtonPrefab,
            HealthUpgradeButtonView healthUpgradeButtonPrefab, HealingButtonView healingButtonPrefab)
        {
            _powerUpButtonPrefab = powerUpButtonPrefab;
            _powerUpSelectionUIView = powerUpSelectionUIView;
            _healingButtonPrefab = healingButtonPrefab;
            _healthUpgradeButtonPrefab = healthUpgradeButtonPrefab;
            _powerUpSelectionUIView.SetController(this);
            _weaponPowerupButtonList = new List<PowerUpButtonView>();
            SubscribeToEvents();
            Hide();
        }

        ~PowerUpSelectionUIController() => UnsubscribeToEvents();

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
            EventService.Instance.OnPowerUpSelection.AddListener(Show);
            EventService.Instance.OnWeaponAdded.AddListener(CreateWeaponPowerUpButtons);
            EventService.Instance.OnGameOver.AddListener(OnGameOver);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnPowerUpSelection.RemoveListener(Show);
            EventService.Instance.OnWeaponAdded.RemoveListener(CreateWeaponPowerUpButtons);
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
            GameService.Instance.ChangeGameState(GameState.Gameplay);
            _powerUpSelectionUIView.DisableView();
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void CreateWeaponPowerUpButtons(List<WeaponController> weaponList)
        {
            _weaponPowerupButtonList.Clear();
            
            foreach (WeaponController weapon in weaponList)
            {
                var newButton = _powerUpSelectionUIView.AddWeaponPowerUpButton(_powerUpButtonPrefab);
                newButton.SetOwner(this);        
                newButton.InitializeView(weapon);
                _weaponPowerupButtonList.Add(newButton);
            }

            CreateHealthPowerUpButtons();
        }

        public void CreateHealthPowerUpButtons()
        {
            _healingButtonView = _powerUpSelectionUIView.AddHealingButton(_healingButtonPrefab);
            _healingButtonView.SetOwner(this);
            _healingButtonView.InitializeView();

            _healthUpgradeButtonView = _powerUpSelectionUIView.AddHealthUpgradeButton(_healthUpgradeButtonPrefab);
            _healthUpgradeButtonView.SetOwner(this);
            _healthUpgradeButtonView.InitializeView();
        }

        public void UpdatePowerUpButtons()
        {
            foreach (var button in _weaponPowerupButtonList)
            {
                button.SetPowerUpButtonData();
            }

            _healthUpgradeButtonView.SetHealthPowerUpButtonData();
            _healingButtonView.SetHealingButtonData();
        }

        public void OnWeaponPowerUpSelected(WeaponController weaponObj)
        {
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.WeaponUpgradeButtonClick);
            weaponObj.ActivateUpgradeWeapon();
            UpdatePowerUpButtons();
            Hide();
           
        }

        public void OnHealingSelected()
        {
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.HealButtonClick);
            GameService.Instance.GetService<PlayerService>().GetPlayer().Heal();
            Hide();
        }

        public void OnHealthUpgradeSelected(int value)
        {
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.HealthUpgradeButtonClick);
            GameService.Instance.GetService<PlayerService>().GetPlayer().UpgradeMaxHealth(value);
            Hide();
        }

        public void OnSkipUpgrade()
        {
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.ButtonClick);
            Hide();
        }

        private void OnGameOver()
        {
            foreach (var button in _weaponPowerupButtonList)
            {
                button.OnDestroy();
            }
            _healingButtonView.OnDestroy();
            _healthUpgradeButtonView.OnDestroy();
        }

    }
}
