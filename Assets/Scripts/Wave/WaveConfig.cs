using Roguelike.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Wave
{
    [System.Serializable]
    public class WaveConfig
    {
        public EnemyScriptableObject enemy_SO;
        public float spawnInitialInterval;
        public int spawnFrequencyPerWave;
        [HideInInspector] public List<Coroutine> spawnCoroutines = new List<Coroutine>();
    }
}
