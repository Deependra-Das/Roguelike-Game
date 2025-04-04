using UnityEngine;

namespace Roguelike.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileScriptableObject", menuName = "ScriptableObjects/ProjectileScriptableObject")]
    public class ProjectileScriptableObject : ScriptableObject
    {
        public ProjectileType projectileType;
        public GameObject projectilePrefab;
    }
}
