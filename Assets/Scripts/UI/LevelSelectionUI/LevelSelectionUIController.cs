using UnityEngine;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Level;
using Roguelike.Sound;

namespace Roguelike.UI
{
    public class LevelSelectionUIController : IUIController
    {
        private LevelButtonView _levelButtonPrefab;
        private List<LevelScriptableObject> _level_SO;
        private LevelSelectionUIView _levelSelectionUIView;
        private List<LevelButtonView> _levelButtons = new List<LevelButtonView>();
        private GameState _currentGameState;

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
            UpdateButonsView();
        }

        public void Hide()
        {
            _levelSelectionUIView.DisableView();
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void CreateLevelButtons()
        {
            _levelButtons.Clear();
           foreach (var levelData in _level_SO)
            {
                var newButton = _levelSelectionUIView.AddButton(_levelButtonPrefab);
                newButton.SetOwner(this);
                newButton.SetLevelButtonData(levelData);
                _levelButtons.Add(newButton);
            }
        }

        public void UpdateButonsView()
        {
            foreach (var levelButtton in _levelButtons)
            {
                levelButtton.SetHighScore();
            }
        }

        public void OnLevelSelected(int levelId)
        {
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.ButtonClick);
            Hide();
            EventService.Instance.OnLevelSelected.Invoke(levelId);
            GameService.Instance.ChangeGameState(GameState.CharacterSelection);
        }

    }
}
