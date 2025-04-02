using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Roguelike.Player;
using Roguelike.Weapon;

namespace Roguelike.UI
{
    public class PowerUpButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _weaponNameText;
        [SerializeField] private TextMeshProUGUI _powerUpText;
        [SerializeField] private Image _weaponImage;


        private PowerUpSelectionUIController owner;
        private WeaponController _weaponObj;

        private void Start() => GetComponent<Button>().onClick.AddListener(OnPowerUpButtonClicked);

        public void SetOwner(PowerUpSelectionUIController owner) => this.owner = owner;

        private void OnPowerUpButtonClicked() => owner.OnWeaponPowerUpSelected(_weaponObj);

        public void InitializeView(WeaponController weaponObj)
        {
            _weaponObj = weaponObj;
            SetPowerUpButtonData();
        }

        public void SetPowerUpButtonData()
        {
            int currentWeaponLevel = _weaponObj.CurrentWeaponLevel;
            _weaponNameText.text = _weaponObj.Weapon_SO.weaponName.ToString();
            _powerUpText.text = _weaponObj.Weapon_SO.powerUp_SO.powerUpList[currentWeaponLevel].description.ToString();
        }

        public void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(OnPowerUpButtonClicked);
            Destroy(gameObject);
        }
    }
}
