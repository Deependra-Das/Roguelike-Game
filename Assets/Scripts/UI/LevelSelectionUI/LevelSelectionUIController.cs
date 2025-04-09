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
        private LevelScriptableObject _level_SO;
        private LevelSelectionUIView _levelSelectionUIView;
        private List<LevelButtonView> _levelButtons = new List<LevelButtonView>();
        private GameState _currentGameState;

        public LevelSelectionUIController(LevelSelectionUIView levelSelectionUIPrefab, LevelButtonView levelButtonPrefab, Transform uiCanvasTransform, LevelScriptableObject level_SO)
        {
            _level_SO = level_SO;
            _levelButtonPrefab = levelButtonPrefab;
            _levelSelectionUIView = Object.Instantiate(levelSelectionUIPrefab, uiCanvasTransform);
            _levelSelectionUIView.SetController(this);
            CreateLevelButtons();
            SubscribeToEvents();
            Hide();
        }

        ~LevelSelectionUIController() => UnsubscribeToEvents();

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
           foreach (var levelData in _level_SO.levelDataList)
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
            ServiceLocator.Instance.GetService<SoundService>().PlaySFX(SoundType.ButtonClick);
            Hide();
            EventService.Instance.OnLevelSelected.Invoke(levelId);
            GameService.Instance.ChangeGameState(GameState.CharacterSelection);
        }

    }
}
