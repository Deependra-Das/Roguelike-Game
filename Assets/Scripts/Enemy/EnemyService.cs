using UnityEngine;
using System.Collections.Generic;
using System;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Utilities;
using Roguelike.Player;

namespace Roguelike.Enemy
{
    public class EnemyService : IService
    {
        private List<EnemyScriptableObject> _enemySO_List;
        private EnemyController _enemyController;
        private EnemyPool _enemyPoolObj;
        private GameState _currentGameState;

        public EnemyService(List<EnemyScriptableObject> enemySO_List)
        {
            _enemySO_List = enemySO_List;
        }

        ~EnemyService() => UnsubscribeToEvents();

        public void Initialize(params object[] dependencies)
        {
            _enemyPoolObj = new EnemyPool();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
            EventService.Instance.OnMainMenu.AddListener(ResetPool);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnMainMenu.RemoveListener(ResetPool);
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public EnemyController SpawnEnemy(int enemyId, Vector2 spawnPosition)
        {
            EnemyController enemy = FetchEnemy(enemyId);
            enemy.Configure(spawnPosition);
            return enemy;
        }

        private EnemyController FetchEnemy(int enemyId)
        {
            EnemyScriptableObject fetchedData = _enemySO_List.Find(item => item.enemyID == enemyId);

            switch (fetchedData.enemyType)
            {
                case EnemyType.Bomber:
                    return (EnemyController)_enemyPoolObj.GetEnemy<BomberEnemyController>(fetchedData);
                case EnemyType.Stalker:
                    return (EnemyController)_enemyPoolObj.GetEnemy<StalkerEnemyController>(fetchedData);
                case EnemyType.Shooter:
                    return (EnemyController)_enemyPoolObj.GetEnemy<ShooterEnemyController>(fetchedData);
                case EnemyType.Boss:
                    return (EnemyController)_enemyPoolObj.GetEnemy<BossEnemyController>(fetchedData);
                default:
                    throw new Exception($"Failed to Create EnemyController for: {fetchedData.enemyType}");
            }
        }

        public void ReturnEnemyToPool(EnemyController enemyToReturn) => _enemyPoolObj.ReturnItem(enemyToReturn);

        private void ResetPool()
        {
            _enemyPoolObj.ResetPool();
        }
    }
}
