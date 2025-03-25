using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Roguelike.Player;

namespace Roguelike.UI
{
    public class PowerUpButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _powerUpNameText;

        private PowerUpSelectionUIController owner;
        private int _powerUpId;

        private void Start() => GetComponent<Button>().onClick.AddListener(OnPowerUpButtonClicked);

        public void SetOwner(PowerUpSelectionUIController owner) => this.owner = owner;

        private void OnPowerUpButtonClicked() => owner.OnPowerUpSelected(_powerUpId);

        public void SetPowerUpButtonData()
        {
        }
    }
}
