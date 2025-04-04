using Roguelike.Weapon;
using UnityEngine;
using System.Collections.Generic;

namespace Roguelike.PowerUp
{
    [CreateAssetMenu(fileName = "PowerUpScriptableObject", menuName = "ScriptableObjects/PowerUpScriptableObject")]
    public class PowerUpScriptableObject : ScriptableObject
    {
        public List<PowerUp> powerUpList;
    }
}
