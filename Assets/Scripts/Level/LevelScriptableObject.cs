using UnityEngine;

namespace Roguelike.Level
{
    [CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObjects/LevelScriptableObject")]
    public class LevelScriptableObject : ScriptableObject
    {
        public int ID;
        public GameObject levelPrefab;
        public int numberOfWaves;
    }
}
