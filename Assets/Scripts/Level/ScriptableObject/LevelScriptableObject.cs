using UnityEngine;
using System.Collections.Generic;

namespace Roguelike.Level
{
    [CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObjects/LevelScriptableObject")]
    public class LevelScriptableObject : ScriptableObject
    {
        public List<LevelData> levelDataList;
    }
}
