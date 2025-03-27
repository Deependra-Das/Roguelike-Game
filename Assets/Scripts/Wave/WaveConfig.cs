using Roguelike.Enemy;
using UnityEngine;

namespace Roguelike.Wave
{
    [System.Serializable]
    public class WaveConfig
    {
        public EnemyScriptableObject enemy_SO;
        public float spawnInterval;
        public int spawnFrequencyPerWave;
    }
}
