using UnityEngine;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Level;

namespace Roguelike.UI
{
    public class LevelSelectionUIController : IUIController
    {
        private LevelButtonView _levelButtonPrefab;
        private List<LevelScriptableObject> _level_SO;
        private LevelSelectionUIView _levelSelectionUIView;
        public GameState CurrentGameState { get; private set; }


        public LevelSelectionUIController(LevelSelectionUIView levelSelectionUIView, LevelButtonView levelButtonPrefab, List<LevelScriptableObject> level_SO)
        {
            _level_SO = level_SO;
            _levelButtonPrefab = levelButtonPrefab;
            _levelSelectionUIView = levelSelectionUIView;
            _levelSelectionUIView.SetController(this);
        }

        ~LevelSelectionUIController() => UnsubscribeToEvents();

        public void InitializeController()
        {
            CreateLevelButtons();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnLevelSelection.AddListener(Show);
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnLevelSelection.RemoveListener(Show);
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
        }

        public void Show()
        {
            _levelSelectionUIView.EnableView();
        }

        public void Hide()
        {
            _levelSelectionUIView.DisableView();
        }

        public void SetGameState(GameState _newState)
        {
            CurrentGameState = _newState;
        }

        public void CreateLevelButtons()
        {
           foreach(var levelData in _level_SO)
            {
                var newButton = _levelSelectionUIView.AddButton(_levelButtonPrefab);
                newButton.SetOwner(this);
                newButton.SetLevelButtonData(levelData);
            }
        }

        public void OnLevelSelected(int levelId)
        {
            Hide();
            EventService.Instance.OnLevelSelected.Invoke(levelId);
            GameService.Instance.ChangeGameState(GameState.CharacterSelection);
        }

    }
}
