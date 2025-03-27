using Roguelike.Wave;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
        public float waveInterval;
        public List<WaveConfig> enemyWaveData;
    }
}
