using UnityEngine;
using UnityEngine.UI;
using Roguelike.PowerUp;

namespace Roguelike.Weapon
{
    [CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/WeaponScriptableObject")]
    public class WeaponScriptableObject : ScriptableObject
    {
        public WeaponType weaponType;
        public GameObject weaponPrefab;
        public Image weaponImage;
        public string weaponName;
        public string weaponDescription;
        public int attackPower;
        public float minRadius;
        public float maxRadius;
        public float cycleTime;
        public float lifeTime;
        public float speed;
        public PowerUpScriptableObject powerUp_SO;
    }

}
