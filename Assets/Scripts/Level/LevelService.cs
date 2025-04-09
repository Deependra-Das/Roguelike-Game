using UnityEngine;
using System.Collections.Generic;
using Roguelike.Main;
using Roguelike.Utilities;
using Roguelike.Event;
using Roguelike.Sound;

namespace Roguelike.Level
{
    public class LevelService : IService
    {
        private Dictionary<int, LevelData> _levelDataDictionary;
        private List<int> _expToUpgradeList;
        private GameObject _activeLevelObj;
        private GameState _currentGameState;
        public int LevelIdSelected { get; private set; }

        public LevelService(LevelScriptableObject levelScriptableObject)
        {
            ConvertLevelDataListToDictionary(levelScriptableObject.levelDataList);
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
            LevelIdSelected = levelID;
        }

        public void LoadLevel()
        {
            LevelData levelData = _levelDataDictionary[LevelIdSelected];

            _activeLevelObj=Object.Instantiate(levelData.levelPrefab);
            switch(LevelIdSelected)
            {
                case 1:
                    ServiceLocator.Instance.GetService<SoundService>().PlayBGM(SoundType.BGM1, true);
                    break;
                case 2:
                    ServiceLocator.Instance.GetService<SoundService>().PlayBGM(SoundType.BGM2, true);
                    break;
                case 3:
                    ServiceLocator.Instance.GetService<SoundService>().PlayBGM(SoundType.BGM3, true);
                    break;
            }
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public LevelData GetLevelData()
        {
            return _levelDataDictionary[LevelIdSelected];
        }

        public List<int> GetExpToUpgradeList()
        {
            return _levelDataDictionary[LevelIdSelected].expToUpgrade_SO.expToUpgradeList;
        }

        private void UnloadLevel()
        {
            LevelIdSelected = -1;
            Object.Destroy(_activeLevelObj);
        }

        private void ConvertLevelDataListToDictionary(List<LevelData> levelDataList)
        {
            _levelDataDictionary = new Dictionary<int, LevelData>();

            foreach (var level in levelDataList)
            {
                _levelDataDictionary.Add(level.ID, level);
            }
        }
    }
}
