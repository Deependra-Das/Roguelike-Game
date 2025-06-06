using Roguelike.Main;
using UnityEngine;

namespace Roguelike.Projectile
{
    public class PlayerProjectileController : IProjectile
    {
        private PlayerProjectileView _playerProjectileView;

        public PlayerProjectileController(ProjectileScriptableObject projectileSO)
        {
            InitializeView(projectileSO);
        }

        private void InitializeView(ProjectileScriptableObject projectileSO)
        {
            GameObject projectile = Object.Instantiate(projectileSO.projectilePrefab, new Vector2(100,100), Quaternion.identity);
            _playerProjectileView = projectile.GetComponent<PlayerProjectileView>();
            _playerProjectileView.SetController(this);
        }

        public void Configure(Vector3 spawnPosition, Vector3 direction, int damage, float lifeTime, float speed)
        {
            _playerProjectileView.gameObject.SetActive(true);
            _playerProjectileView.Initialize(spawnPosition, direction, damage, lifeTime, speed);
        }

        public void ReturnProjectileToPool()
        {
            ServiceLocator.Instance.GetService<ProjectileService>().ReturnProjectileToPool(this);
        }

        public GameObject GetProjectileGameObject() { return _playerProjectileView.gameObject; }

    }
}
