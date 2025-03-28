using UnityEngine;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Level;

namespace Roguelike.UI
{
    public class LevelSelectionUIController : IUIController
    {
        private LevelSelectionUIView _levelSelectionUIView;
        [SerializeField] private LevelButtonView _levelButtonPrefab;
        [SerializeField] private List<LevelScriptableObject> _level_SO;

        public LevelSelectionUIController(LevelSelectionUIView levelSelectionUIView, LevelButtonView levelButtonPrefab, List<LevelScriptableObject> level_SO)
        {
            _level_SO = level_SO;
            _levelButtonPrefab = levelButtonPrefab;
            _levelSelectionUIView = levelSelectionUIView;
            _levelSelectionUIView.SetController(this);
        }

        public void InitializeController()
        {
            CreateLevelButtons();
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnNewGameButtonClicked.AddListener(ShowLevelSelectionUI);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnNewGameButtonClicked.RemoveListener(ShowLevelSelectionUI);
        }

        public void ShowLevelSelectionUI() => Show();

        public void Show()
        {
            _levelSelectionUIView.EnableView();
        }

        public void Hide()
        {
            _levelSelectionUIView.DisableView();
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
            GameService.Instance.GetService<EventService>().OnLevelSelected.Invoke(levelId);
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
