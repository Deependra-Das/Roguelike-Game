using UnityEngine;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Player;
using Roguelike.Sound;

namespace Roguelike.UI
{
    public class CharacterSelectionUIController : IUIController
    {
        private CharacterSelectionUIView _characterSelectionUIView;
        private CharacterButtonView _characterButtonPrefab;
        private List<PlayerScriptableObject> _character_SO;
        private GameState _currentGameState;

        public CharacterSelectionUIController(CharacterSelectionUIView characterSelectionUIView, CharacterButtonView characterButtonPrefab, List<PlayerScriptableObject> character_SO)
        {
            _character_SO = character_SO;
            _characterButtonPrefab = characterButtonPrefab;
            _characterSelectionUIView = characterSelectionUIView;
            _characterSelectionUIView.SetController(this);
        }

        ~CharacterSelectionUIController()
        {
            UnsubscribeToEvents();
        }

        public void InitializeController()
        {
            CreateCharacterButtons();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnCharacterSelection.AddListener(Show);
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnCharacterSelection.RemoveListener(Show);
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
        }

        public void Show()
        {
            _characterSelectionUIView.EnableView();
        }

        public void Hide()
        {
            _characterSelectionUIView.DisableView();
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
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
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.ButtonClick);
            EventService.Instance.OnCharacterSelected.Invoke(characterId);
            GameService.Instance.StartGameplay();
            Hide();
        }
    }
}
