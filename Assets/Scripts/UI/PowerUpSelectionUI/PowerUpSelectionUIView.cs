using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Roguelike.UI
{
    public class PowerUpSelectionUIView : MonoBehaviour, IUIView
    {
        private PowerUpSelectionUIController _controller;
        [SerializeField] private Transform _weaponUpgradeButtonContainer;
        [SerializeField] private Transform _heathUpgradeButtonContainer;

        public void SetController(IUIController controllerToSet) => _controller = controllerToSet as PowerUpSelectionUIController;

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView() => gameObject.SetActive(true);

        public PowerUpButtonView AddWeaponPowerUpButton(PowerUpButtonView powerUpButtonPrefab) => Instantiate(powerUpButtonPrefab, _weaponUpgradeButtonContainer);

        public HealingButtonView AddHealingButton(HealingButtonView healingButtonPrefab) => Instantiate(healingButtonPrefab, _heathUpgradeButtonContainer);

        public HealthUpgradeButtonView AddHealthUpgradeButton(HealthUpgradeButtonView healthUpgradeButtonPrefab) => Instantiate(healthUpgradeButtonPrefab, _heathUpgradeButtonContainer);

    }

}
