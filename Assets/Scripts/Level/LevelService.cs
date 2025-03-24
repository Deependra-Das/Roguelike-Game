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
            _levelIdSelected = levelID;
        }

        public void LoadLevel()
        {
            var levelData = levelScriptableObjects.Find(levelSO => levelSO.ID == _levelIdSelected);
            Object.Instantiate(levelData.levelPrefab);
            Debug.Log(levelData.numberOfWaves);
            UnsubscribeToEvents();
        }
    }
}
