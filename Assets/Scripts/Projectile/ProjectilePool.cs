using Roguelike.Projectile;
using Roguelike.Utilities;
using UnityEngine;
using System;

public class ProjectilePool : GenericObjectPool<IProjectile>
{
    private ProjectileScriptableObject _projectileData;

    public IProjectile GetProjectile<T>(ProjectileScriptableObject projectileData) where T : IProjectile
    {
        _projectileData = projectileData;
        return GetItem<T>();
    }

    protected override IProjectile CreateItem<T>()
    {
        if (typeof(T) == typeof(PlayerProjectileController))
        {
            return new PlayerProjectileController(_projectileData);
        }
    
        else
        {
            throw new NotSupportedException("Projectile type not supported");
        }
    }

    public void ResetPool()
    {
        foreach (var pooledItem in pooledItems)
        {
            UnityEngine.Object.Destroy(pooledItem.Item.GetProjectileGameObject());
        }

        pooledItems.Clear();
    }

}
