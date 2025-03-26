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

        public EnemyService(List<EnemyScriptableObject> enemySO_List)
        {
            _enemySO_List = enemySO_List;
        }

        public void Initialize(params object[] dependencies)
        {
            _enemyPoolObj = new EnemyPool(); 
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnStartGame.AddListener(StartSpawning);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnStartGame.RemoveListener(StartSpawning);
        }

        public void StartSpawning()
        {
            SpawnEnemy(1);
        }

        public EnemyController SpawnEnemy(int enemyId)
        {
            EnemyController enemy = FetchEnemy(enemyId);
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

    }
}
