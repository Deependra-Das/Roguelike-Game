using UnityEngine;

namespace Roguelike.Projectile
{
    public interface IProjectile
    {
        public void Configure(Vector3 spawnPosition, Vector3 direction, int damage, float lifeTime, float speed);
        public void ReturnProjectileToPool();
        public GameObject GetProjectileGameObject();
    }
}