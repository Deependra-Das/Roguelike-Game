using Roguelike.Wave;
using UnityEngine;
using System.Collections.Generic;

namespace Roguelike.Level
{
    [System.Serializable]
    public class LevelData
    {
        public int ID;
        public GameObject levelPrefab;
        public Sprite levelImage;
        public string levelName;
        public string levelDescription;
        public float spawnIntervalDecrementRate;
        public float spawnFrequencyIncreaseFactor;
        public float spawnFinalInterval;
        public float waveInterval;
        public ExpToUpgradeScriptableObject expToUpgrade_SO;
        public List<WaveConfig> enemyWaveData;
    }
}
