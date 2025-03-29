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

        public LevelService(List<LevelScriptableObject> levelScriptableObjects)
        {
            this.levelScriptableObjects = levelScriptableObjects;
        }

        public void Initialize(params object[] dependencies)
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnLevelSelected.AddListener(SelectLevel);
            GameService.Instance.GetService<EventService>().OnStartGame.AddListener(LoadLevel);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnLevelSelected.RemoveListener(SelectLevel);
            GameService.Instance.GetService<EventService>().OnStartGame.RemoveListener(LoadLevel);
        }

        private void SelectLevel(int levelID)
        {
            Debug.Log(levelID);
            _levelIdSelected = levelID;
        }

        public void LoadLevel()
        {
            var levelData = levelScriptableObjects.Find(levelSO => levelSO.ID == _levelIdSelected);
            //InitializeExpToUpgrade(levelData);
            Object.Instantiate(levelData.levelPrefab);
            GameService.Instance.GetService<EventService>().OnStartWaveSpawn.Invoke(levelData.spawnIntervalDecrementRate, levelData.spawnFinalInterval,
                levelData.waveInterval, levelData.enemyWaveData);
            UnsubscribeToEvents();
        }

        //private void InitializeExpToUpgrade(LevelScriptableObject levelData)
        //{
        //    int numberOfLevels = 30;
        //    int baseExp = 10;
        //    int increment1 = 10;  
        //    int increment2 = 20;  
        //    int levelThreshold = 5; 

        //    for (int i = 0; i < numberOfLevels; i++)
        //    {
        //        int expRequired;
        //        if (i <= levelThreshold)
        //        {
        //            expRequired = baseExp + (i * increment1);
        //        }
        //        else
        //        {
        //            expRequired = baseExp + (levelThreshold * increment1) + ((i - levelThreshold) * increment2);
        //        }

        //        levelData.expToUpgrade_SO.expToUpgradeList.Add(expRequired);
        //    }
        //}
    }
}
