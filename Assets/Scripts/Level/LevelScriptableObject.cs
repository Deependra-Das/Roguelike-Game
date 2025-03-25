using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.Level
{
    [CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObjects/LevelScriptableObject")]
    public class LevelScriptableObject : ScriptableObject
    {
        public int ID;
        public GameObject levelPrefab;
        public Image levelImage;
        public string levelName;
        public string levelDescription;
        public int numberOfWaves;
    }
}
