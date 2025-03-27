using Roguelike.Utilities;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roguelike.Enemy;
using Roguelike.Event;
using Roguelike.Main;

namespace Roguelike.Wave
{
    public class WaveService : MonoBehaviour,IService
    {
        private List<WaveConfig> _waveData;
        private int _currentWaveIndex = 0;
        private bool _isSpawning = false;
        private float _waveInterval;

        public void Initialize(params object[] dependencies)
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnStartWaveSpawn.AddListener(StartWave);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnStartWaveSpawn.RemoveListener(StartWave);
        }

        private void StartWave(float waveInterval, List<WaveConfig> waveData)
        {
            _currentWaveIndex = 0;
            _waveInterval = waveInterval;
            _waveData = waveData;
            StartCoroutine(SpawnWave());
        }

        IEnumerator SpawnWave()
        {
            if (_currentWaveIndex < _waveData.Count)
            {
                WaveConfig waveConfig = _waveData[_currentWaveIndex];

                for (int i = 0; i < waveConfig.spawnFrequencyPerWave; i++)
                {
                    SpawnEnemy(waveConfig.enemy_SO.enemyID);       
                    yield return new WaitForSeconds(waveConfig.spawnInterval);
                }

                _currentWaveIndex++;

                yield return new WaitForSeconds(_waveInterval);

                if (_currentWaveIndex < _waveData.Count)
                {
                    StartCoroutine(SpawnWave());
                }
                else
                {
                    Debug.Log("All waves have been completed!");
                }
            }
        }

        private void SpawnEnemy(int ID)
        {
            GameService.Instance.GetService<EnemyService>().SpawnEnemy(ID);
        }
    }

}
