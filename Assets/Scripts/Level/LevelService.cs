using UnityEngine;
using System.Collections.Generic;
using Roguelike.Main;
using Roguelike.Utilities;
using Roguelike.Event;

namespace Roguelike.Level
{
    public class LevelService : IService
    {
        private List<LevelScriptableObject> levelScriptableObjects;
        private int _levelIdSelected = -1;
        private List<int> _expToUpgradeList;
        private GameObject _activeLevelObj;
        private GameState _currentGameState;


        public LevelService(List<LevelScriptableObject> levelScriptableObjects)
        {
            this.levelScriptableObjects = levelScriptableObjects;
        }

        ~LevelService() => UnsubscribeToEvents();

        public void Initialize(params object[] dependencies)
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
            EventService.Instance.OnLevelSelected.AddListener(SelectLevel);
            EventService.Instance.OnStartGameplay.AddListener(LoadLevel);
            EventService.Instance.OnGameOver.AddListener(UnloadLevel);
            EventService.Instance.OnLevelCompleted.AddListener(UnloadLevel);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnLevelSelected.RemoveListener(SelectLevel);
            EventService.Instance.OnStartGameplay.RemoveListener(LoadLevel);
            EventService.Instance.OnGameOver.RemoveListener(UnloadLevel);
            EventService.Instance.OnLevelCompleted.RemoveListener(UnloadLevel);
        }

        private void SelectLevel(int levelID)
        {
            _levelIdSelected = levelID;
        }

        public void LoadLevel()
        {
            var levelData = levelScriptableObjects.Find(levelSO => levelSO.ID == _levelIdSelected);
            _activeLevelObj=Object.Instantiate(levelData.levelPrefab);
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public LevelScriptableObject GetLevelData()
        {
            return levelScriptableObjects.Find(levelSO => levelSO.ID == _levelIdSelected);
        }

        public List<int> GetExpToUpgradeList()
        {
            return levelScriptableObjects.Find(levelSO => levelSO.ID == _levelIdSelected).expToUpgrade_SO.expToUpgradeList;
        }

        private void UnloadLevel()
        {
            _levelIdSelected = -1;
            Object.Destroy(_activeLevelObj);
        }
    }
}
