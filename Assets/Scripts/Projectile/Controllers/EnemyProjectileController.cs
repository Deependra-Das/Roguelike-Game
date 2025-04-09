using Roguelike.Main;
using UnityEngine;

namespace Roguelike.Projectile
{
    public class EnemyProjectileController : IProjectile
    {
        private EnemyProjectileView _enemyProjectileView;

        public EnemyProjectileController(ProjectileScriptableObject projectileSO)
        {
            InitializeView(projectileSO);
        }

        private void InitializeView(ProjectileScriptableObject projectileSO)
        {
            GameObject projectile = Object.Instantiate(projectileSO.projectilePrefab, new Vector2(100,100), Quaternion.identity);
            _enemyProjectileView = projectile.GetComponent<EnemyProjectileView>();
            _enemyProjectileView.SetController(this);
            _enemyProjectileView.gameObject.SetActive(false);
        }

        public void Configure(Vector3 spawnPosition, Vector3 direction, int damage, float lifeTime, float speed)
        {
            _enemyProjectileView.gameObject.SetActive(true);
            _enemyProjectileView.Initialize(spawnPosition, direction, damage, lifeTime, speed);
        }

        public void ReturnProjectileToPool()
        {
            ServiceLocator.Instance.GetService<ProjectileService>().ReturnProjectileToPool(this);
        }

        public GameObject GetProjectileGameObject() { return _enemyProjectileView.gameObject; }

    }
}
