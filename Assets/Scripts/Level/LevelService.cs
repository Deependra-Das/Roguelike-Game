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

        public LevelService(List<LevelScriptableObject> levelScriptableObjects)
        {
            this.levelScriptableObjects = levelScriptableObjects;
        }

        public void Initialize(params object[] dependencies)
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents() => GameService.Instance.GetService<EventService>().OnLevelSelected.AddListener(LoadLevel);

        private void UnsubscribeToEvents() => GameService.Instance.GetService<EventService>().OnLevelSelected.RemoveListener(LoadLevel);

        public void LoadLevel(int levelID)
        {
            var levelData = levelScriptableObjects.Find(levelSO => levelSO.ID == levelID);
            Object.Instantiate(levelData.levelPrefab);
            Debug.Log(levelData.numberOfWaves);
            UnsubscribeToEvents();
        }
    }
}
