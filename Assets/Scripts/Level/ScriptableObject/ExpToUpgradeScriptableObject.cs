using UnityEngine;
using System.Collections.Generic;

namespace Roguelike.Level
{
    [CreateAssetMenu(fileName = "ExpToUpgradeScriptableObject", menuName = "ScriptableObjects/ExpToUpgradeScriptableObject")]
    public class ExpToUpgradeScriptableObject : ScriptableObject
    {
        public List<int> expToUpgradeList;
    }
}
