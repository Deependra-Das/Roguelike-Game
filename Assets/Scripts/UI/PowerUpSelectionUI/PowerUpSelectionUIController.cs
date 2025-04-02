using UnityEngine;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Weapon;
using Roguelike.Projectile;

namespace Roguelike.UI
{
    public class PowerUpSelectionUIController : IUIController
    {
        private PowerUpSelectionUIView _powerUpSelectionUIView;
        private PowerUpButtonView _powerUpButtonPrefab;
        private List<IWeapon> weaponList;
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
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
            EventService.Instance.OnPowerUpSelection.AddListener(Show);
            EventService.Instance.OnWeaponAdded.AddListener(CreatePowerUpButtons);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnPowerUpSelection.RemoveListener(Show);
            EventService.Instance.OnWeaponAdded.RemoveListener(CreatePowerUpButtons);
        }


        public void Show()
        {
            _powerUpSelectionUIView.EnableView();
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

        public void CreatePowerUpButtons(List<IWeapon> weaponList)
        {
            foreach (IWeapon weapons in weaponList)
            {
                var newButton = _powerUpSelectionUIView.AddButton(_powerUpButtonPrefab);
                newButton.SetOwner(this);

                switch(weapons)
                {
                    case RadialReapWeapon:
                        Debug.Log("RR");
                        break;
                    case OrbitalFuryWeapon:
                        Debug.Log("OF");
                        break;
                    case ScatterShotWeapon:
                        Debug.Log("SS");
                        break;
                }
                //newButton.SetPowerUpButtonData(weapons);
            }
        }

        public void OnPowerUpSelected()
        {
            Hide();
            GameService.Instance.ChangeGameState(GameState.Gameplay);
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
