using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Roguelike.Player;
using Roguelike.Weapon;
using Roguelike.PowerUp;
using Roguelike.Main;

namespace Roguelike.UI
{
    public class HealingButtonView : MonoBehaviour
    {
        private PowerUpSelectionUIController owner;
        [SerializeField] private Button _healingButton;
        [SerializeField] private GameObject _notAvaialableBanner;

        private void OnEnable() => _healingButton.onClick.AddListener(OnHealingButtonClicked);

        private void OnDisable() => _healingButton.onClick.RemoveListener(OnHealingButtonClicked);

        public void SetOwner(PowerUpSelectionUIController owner) => this.owner = owner;

        public void InitializeView() { }

        public void SetHealingButtonData()
        {
            EnableButton();
            _notAvaialableBanner.SetActive(false);
            int curHealth = GameService.Instance.GetService<PlayerService>().GetPlayer().PlayerModel.CurrentHealth;
            int maxHealth = GameService.Instance.GetService<PlayerService>().GetPlayer().PlayerModel.MaxHealth;
            if (curHealth == maxHealth)
            {
                _notAvaialableBanner.SetActive(true);
                DisableButton();
            }
        }

        private void OnHealingButtonClicked() 
        {
            owner.OnHealingSelected();
        }

        public void DisableButton()
        {
            _healingButton.interactable = false;
        }

        public void EnableButton()
        {
            _healingButton.interactable = true;
        }

        public void OnDestroy()
        {
            _healingButton.onClick.RemoveListener(OnHealingButtonClicked);
            Destroy(gameObject);
        }
    }
}
