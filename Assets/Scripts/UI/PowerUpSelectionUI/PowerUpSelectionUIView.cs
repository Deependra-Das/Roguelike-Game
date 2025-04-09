using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Roguelike.Main;
using Roguelike.Player;

namespace Roguelike.UI
{
    public class PowerUpSelectionUIView : MonoBehaviour
    {
        private PowerUpSelectionUIController _controller;
        [SerializeField] private Transform _weaponUpgradeButtonContainer;
        [SerializeField] private Transform _heathUpgradeButtonContainer;
        [SerializeField] private Button _skipUpgradeButton;
        [SerializeField] private int AllowSkipUpgradeAtLevel;

        private void OnEnable() => _skipUpgradeButton.onClick.AddListener(OnSkipUpgradeButtonClicked);

        private void OnDisable() => _skipUpgradeButton.onClick.RemoveListener(OnSkipUpgradeButtonClicked);

        public void SetController(IUIController controllerToSet) => _controller = controllerToSet as PowerUpSelectionUIController;   

        public void DisableView() => gameObject.SetActive(false);

        public void EnableView()
        {
            gameObject.SetActive(true);
            SetViewData();
        }

        public PowerUpButtonView AddWeaponPowerUpButton(PowerUpButtonView powerUpButtonPrefab) => Instantiate(powerUpButtonPrefab, _weaponUpgradeButtonContainer);

        public HealingButtonView AddHealingButton(HealingButtonView healingButtonPrefab) => Instantiate(healingButtonPrefab, _heathUpgradeButtonContainer);

        public HealthUpgradeButtonView AddHealthUpgradeButton(HealthUpgradeButtonView healthUpgradeButtonPrefab) => Instantiate(healthUpgradeButtonPrefab, _heathUpgradeButtonContainer);

        public void OnSkipUpgradeButtonClicked()
        {
            _controller.OnSkipUpgrade();
        }

        private void SetViewData()
        {
            int playerExpLevel = ServiceLocator.Instance.GetService<PlayerService>().GetPlayer().PlayerModel.CurrentExpLevel;

            if (playerExpLevel <= AllowSkipUpgradeAtLevel)
            {
                _skipUpgradeButton.gameObject.SetActive(false);
            }
            else
            {
                _skipUpgradeButton.gameObject.SetActive(true);
            }
        }

    }

}
