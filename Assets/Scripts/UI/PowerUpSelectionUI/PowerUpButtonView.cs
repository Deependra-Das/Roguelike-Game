using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Roguelike.Player;
using Roguelike.Weapon;

namespace Roguelike.UI
{
    public class PowerUpButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _powerUpNameText;

        private PowerUpSelectionUIController owner;
        private WeaponType _weaponType;

        private void Start() => GetComponent<Button>().onClick.AddListener(OnPowerUpButtonClicked);

        public void SetOwner(PowerUpSelectionUIController owner) => this.owner = owner;

        private void OnPowerUpButtonClicked() => owner.OnPowerUpSelected();

        public void SetPowerUpButtonData()
        {
        }
    }
}
