using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Roguelike.Player;

namespace Roguelike.UI
{
    public class CharacterButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _characterNameText;

        private CharacterSelectionUIController owner;
        private int _characterId;

        private void Start() => GetComponent<Button>().onClick.AddListener(OnCharacterButtonClicked);

        public void SetOwner(CharacterSelectionUIController owner) => this.owner = owner;

        private void OnCharacterButtonClicked() => owner.OnCharacterSelected(_characterId);

        public void SetCharacterButtonData(PlayerScriptableObject characterData)
        {
            _characterId = characterData.ID;
            _characterNameText.SetText(characterData.characterName);
        }
    }
}
