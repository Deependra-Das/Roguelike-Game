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
        private PlayerScriptableObject _player_SO;
        private GameState _currentGameState;

        public CharacterSelectionUIController(CharacterSelectionUIView characterSelectionUIPrefab, CharacterButtonView characterButtonPrefab, Transform uiCanvasTransform, PlayerScriptableObject player_SO)
        {
            _player_SO = player_SO;
            _characterButtonPrefab = characterButtonPrefab;
            _characterSelectionUIView = Object.Instantiate(characterSelectionUIPrefab, uiCanvasTransform);
            _characterSelectionUIView.SetController(this);
            CreateCharacterButtons();
            SubscribeToEvents();
            Hide();
        }

        ~CharacterSelectionUIController()
        {
            UnsubscribeToEvents();
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
            foreach (var characterData in _player_SO.playerDataList)
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
