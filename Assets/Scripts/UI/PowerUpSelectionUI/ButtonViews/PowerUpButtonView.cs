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
        [SerializeField] private Button _powerUpButton;
        [SerializeField] private GameObject _maxLevelBanner;

        private PowerUpSelectionUIController owner;
        private WeaponController _weaponObj;

        private void Start() => _powerUpButton.onClick.AddListener(OnPowerUpButtonClicked);

        public void SetOwner(PowerUpSelectionUIController owner) => this.owner = owner;

        private void OnPowerUpButtonClicked() => owner.OnWeaponPowerUpSelected(_weaponObj);

        public void InitializeView(WeaponController weaponObj)
        {
            _weaponObj = weaponObj;
            SetPowerUpButtonData();
        }

        public void SetPowerUpButtonData()
        {
            EnableButton();
            _maxLevelBanner.SetActive(false);
            _weaponImage.sprite = _weaponObj.Weapon_SO.weaponImage;
            int currentWeaponLevel = _weaponObj.CurrentWeaponLevel;

            if (_weaponObj.CurrentWeaponLevel>= _weaponObj.Weapon_SO.powerUp_SO.powerUpList.Count)
            {
                _maxLevelBanner.SetActive(true);
                DisableButton();
            }
            else
            {
                _weaponNameText.text = _weaponObj.Weapon_SO.weaponName.ToString();
                _powerUpText.text = _weaponObj.Weapon_SO.powerUp_SO.powerUpList[currentWeaponLevel].description.ToString();
            }
        }

        public void OnDestroy()
        {
            _powerUpButton.onClick.RemoveListener(OnPowerUpButtonClicked);
            Destroy(gameObject);
        }

        public void DisableButton()
        {
            _powerUpButton.interactable = false;
        }

        public void EnableButton()
        {
            _powerUpButton.interactable = true;
        }
    }
}
