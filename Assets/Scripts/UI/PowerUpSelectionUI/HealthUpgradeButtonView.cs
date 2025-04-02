using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Roguelike.Player;
using Roguelike.Weapon;
using Roguelike.PowerUp;

namespace Roguelike.UI
{
    public class HealthUpgradeButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _powerUpText;
        [SerializeField] private PowerUpScriptableObject _healthPowerUp_SO;

        private int currentHealthLevel;

        private PowerUpSelectionUIController owner;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnHealthUpgradeButtonClicked);
        }

        public void InitializeView()
        {
            currentHealthLevel = 0;
            SetHealthPowerUpButtonData();
        }

        public void SetOwner(PowerUpSelectionUIController owner) => this.owner = owner;

        public void SetHealthPowerUpButtonData()
        {
            _powerUpText.text = _healthPowerUp_SO.powerUpList[currentHealthLevel].description.ToString();
        }

        private void OnHealthUpgradeButtonClicked() 
        { 
            int newMaxHealthToUpdate = (int)_healthPowerUp_SO.powerUpList[currentHealthLevel].value;
            currentHealthLevel++;
            owner.OnHealthUpgradeSelected(newMaxHealthToUpdate);
        }

        public void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(OnHealthUpgradeButtonClicked);
            Destroy(gameObject);
        }
    }
}
