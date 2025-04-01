using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Utilities;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Roguelike.Projectile
{
    public class ProjectileService : IService
    {
        private ProjectilePool _projectilePoolObj;
        private GameState _currentGameState;
        private List<ProjectileScriptableObject> _projectileSO_List;


        public ProjectileService(List<ProjectileScriptableObject> projectileSO_List) 
        {
            _projectileSO_List=projectileSO_List;
        }

        ~ProjectileService() => UnsubscribeToEvents();

        public void Initialize(params object[] dependencies)
        {
            _projectilePoolObj = new ProjectilePool();
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

        public IProjectile SpawnProjectile(ProjectileType projectileType, Vector2 spawnPosition, Vector3 direction, int damage, float lifeTime, float speed)
        {
            IProjectile projectile = FetchProjectile(projectileType);
            projectile.Configure(spawnPosition, direction, damage, lifeTime, speed);
            return projectile;
        }

        private IProjectile FetchProjectile(ProjectileType type)
        {
            ProjectileScriptableObject fetchedData = _projectileSO_List.Find(item => item.projectileType == type);

            switch (fetchedData.projectileType)
            {
                case ProjectileType.PlayerBall:
                    return (IProjectile)_projectilePoolObj.GetProjectile<PlayerProjectileController>(fetchedData);
   
                default:
                    throw new Exception($"Failed to Create IProjectile class for: {fetchedData.projectileType}");
            }
        }

        public void ReturnProjectileToPool(IProjectile projectileToReturn) => _projectilePoolObj.ReturnItem(projectileToReturn);

        private void ResetPool()
        {
            _projectilePoolObj.ResetPool();
        }
    }
}

