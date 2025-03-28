using UnityEngine;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Player;

namespace Roguelike.UI
{
    public class CharacterSelectionUIController : IUIController
    {
        private CharacterSelectionUIView _characterSelectionUIView;
        [SerializeField] private CharacterButtonView _characterButtonPrefab;
        [SerializeField] private List<PlayerScriptableObject> _character_SO;

        public CharacterSelectionUIController(CharacterSelectionUIView characterSelectionUIView, CharacterButtonView characterButtonPrefab, List<PlayerScriptableObject> character_SO)
        {
            _character_SO = character_SO;
            _characterButtonPrefab = characterButtonPrefab;
            _characterSelectionUIView = characterSelectionUIView;
            _characterSelectionUIView.SetController(this);
        }

        public void InitializeController()
        {
            CreateCharacterButtons();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnLevelSelected.AddListener(ShowCharacterSelectionUI);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnLevelSelected.RemoveListener(ShowCharacterSelectionUI);
        }

        public void ShowCharacterSelectionUI(int levelId) => Show();

        public void Show()
        {
            _characterSelectionUIView.EnableView();
        }

        public void Hide()
        {
            _characterSelectionUIView.DisableView();
        }

        public void CreateCharacterButtons()
        {
            foreach (var characterData in _character_SO)
            {
                var newButton = _characterSelectionUIView.AddButton(_characterButtonPrefab);
                newButton.SetOwner(this);
                newButton.SetCharacterButtonData(characterData);
            }
        }

        public void OnCharacterSelected(int characterId)
        {
            GameService.Instance.GetService<EventService>().OnCharacterSelected.Invoke(characterId);
            GameService.Instance.GetService<EventService>().OnStartGame.Invoke();
            Hide();
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
