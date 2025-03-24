using Roguelike.Event;
using Roguelike.Main;
using UnityEngine;
using System.Collections.Generic;

namespace Roguelike.UI
{
    public class LevelSelectionUIController : IUIController
    {
        private LevelSelectionUIView _levelSelectionUIView;
        [SerializeField] private LevelButtonView _levelButtonPrefab;
        [SerializeField] private List<LevelButtonScriptableObject> _levelButton_SO;

        public LevelSelectionUIController(LevelSelectionUIView levelSelectionUIView, LevelButtonView levelButtonPrefab, List<LevelButtonScriptableObject> levelButton_SO)
        {
            _levelButton_SO = levelButton_SO;
            _levelButtonPrefab = levelButtonPrefab;
            _levelSelectionUIView = levelSelectionUIView;
            _levelSelectionUIView.SetController(this);
        }

        public void InitializeController()
        {
            CreateLevelButtons(_levelButton_SO);
            SubscribeToEvents();
            Hide();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnNewGameButtonSelected.AddListener(ShowLevelSelectionUI);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnNewGameButtonSelected.RemoveListener(ShowLevelSelectionUI);
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

        public void CreateLevelButtons(List<LevelButtonScriptableObject> _levelButton_SO)
        {
           foreach(var levelData in _levelButton_SO)
            {
                var newButton = _levelSelectionUIView.AddButton(_levelButtonPrefab);
                newButton.SetOwner(this);
                newButton.SetLevelButtonData(levelData);
            }
        }

        public void OnLevelSelected(int levelId)
        {
            GameService.Instance.GetService<EventService>().OnLevelSelected.Invoke(levelId);
            Hide();
        }

        private void OnDestroy()
        {
            UnsubscribeToEvents();
        }
    }
}
