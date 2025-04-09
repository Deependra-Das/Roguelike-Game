using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Roguelike.Player;

namespace Roguelike.UI
{
    public class CharacterButtonView : MonoBehaviour
    {
        [SerializeField] private Button _characterButton;
        [SerializeField] private TextMeshProUGUI _characterNameText;
        [SerializeField] private Image _characterImage;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _speedSlider;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _maxSpeed;

        private CharacterSelectionUIController owner;
        private int _characterId;

        private void OnEnable() => _characterButton.onClick.AddListener(OnCharacterButtonClicked);

        private void OnDisable() => _characterButton.onClick.RemoveListener(OnCharacterButtonClicked);

        public void SetOwner(CharacterSelectionUIController owner) => this.owner = owner;

        private void OnCharacterButtonClicked() => owner.OnCharacterSelected(_characterId);

        public void SetCharacterButtonData(PlayerData characterData)
        {
            _characterId = characterData.ID;
            _characterNameText.text =characterData.characterName;
            _characterImage.sprite = characterData.playerImage;
            _healthSlider.maxValue = _maxHealth;
            _healthSlider.value = characterData.maxHealth;
            _speedSlider.maxValue = _maxSpeed;
            _speedSlider.value = characterData.movementSpeed;
            
        }
    }
}
