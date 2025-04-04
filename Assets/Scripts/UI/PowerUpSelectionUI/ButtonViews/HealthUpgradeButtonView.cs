using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Roguelike.Player;
using Roguelike.Weapon;
using Roguelike.PowerUp;
using Roguelike.Main;

namespace Roguelike.UI
{
    public class HealthUpgradeButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _powerUpText;
        [SerializeField] private PowerUpScriptableObject _healthPowerUp_SO;
        [SerializeField] private Button _healthUpgradeButton;
        [SerializeField] private GameObject _maxLevelBanner;
        [SerializeField] private GameObject _notAvailableBanner;
        [SerializeField] private int UnlockHealthUpgradeAtLevel;

        private int _currentHealthLevel;

        private PowerUpSelectionUIController owner;

        private void OnEnable() => _healthUpgradeButton.onClick.AddListener(OnHealthUpgradeButtonClicked);

        private void OnDisable() => _healthUpgradeButton.onClick.RemoveListener(OnHealthUpgradeButtonClicked);

        public void InitializeView()
        {
            _currentHealthLevel = 0;
        }

        public void SetOwner(PowerUpSelectionUIController owner) => this.owner = owner;

        public void SetHealthPowerUpButtonData()
        {
            EnableButton();
            _maxLevelBanner.SetActive(false);
            _notAvailableBanner.SetActive(false);

            int playerExpLevel = GameService.Instance.GetService<PlayerService>().GetPlayer().PlayerModel.CurrentExpLevel;

            if (playerExpLevel< UnlockHealthUpgradeAtLevel)
            {
                _notAvailableBanner.SetActive(true);
                DisableButton();
            }

            if(_currentHealthLevel>= _healthPowerUp_SO.powerUpList.Count)
            {
                _maxLevelBanner.SetActive(true);
                DisableButton();
            }
            else
            {
                _powerUpText.text = _healthPowerUp_SO.powerUpList[_currentHealthLevel].description.ToString();
            }
        }

        private void OnHealthUpgradeButtonClicked() 
        { 
            int newMaxHealthToUpdate = (int)_healthPowerUp_SO.powerUpList[_currentHealthLevel].value;
            _currentHealthLevel++;
            owner.OnHealthUpgradeSelected(newMaxHealthToUpdate);
        }

        public void DisableButton()
        {
            _healthUpgradeButton.interactable = false;
        }

        public void EnableButton()
        {
            _healthUpgradeButton.interactable = true;
        }

        public void OnDestroy()
        {
            _healthUpgradeButton.onClick.RemoveListener(OnHealthUpgradeButtonClicked);
            Destroy(gameObject);
        }
    }
}
