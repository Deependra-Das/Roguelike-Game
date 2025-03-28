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
        private float _spawnInitialIntervalDecrementRate;
        private float _spawnFinalInterval;
        private int _currentWaveIndex = 0;
        private float _waveInterval;
        private bool isSpawning = false;
        private bool isPaused = false;
        private Coroutine spawnCoroutine;

        public void Initialize(params object[] dependencies)
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnStartWaveSpawn.AddListener(StartWave);
            GameService.Instance.GetService<EventService>().OnPauseGame.AddListener(PauseSpawning);
            GameService.Instance.GetService<EventService>().OnContinueButtonClicked.AddListener(ResumeSpawning);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnStartWaveSpawn.RemoveListener(StartWave);
            GameService.Instance.GetService<EventService>().OnPauseGame.RemoveListener(PauseSpawning);
            GameService.Instance.GetService<EventService>().OnContinueButtonClicked.RemoveListener(ResumeSpawning);
        }

        private void StartWave(float spawnInitialIntervalDecrementRate, float spawnFinalInterval, float waveInterval, List<WaveConfig> waveData)
        {
            _currentWaveIndex = 0;
            _waveData = waveData;
            _spawnInitialIntervalDecrementRate = spawnInitialIntervalDecrementRate;
            _spawnFinalInterval = spawnFinalInterval;
            _waveInterval = waveInterval;

            if (!isSpawning && !isPaused)
            {
                isSpawning = true;
                spawnCoroutine = StartCoroutine(SpawnWaves());
            }
        
        }

        public void StopSpawning()
        {
            if (isSpawning)
            {
                isSpawning = false;

                if (spawnCoroutine != null)
                {
                    StopCoroutine(spawnCoroutine);
                    spawnCoroutine = null;
                }

                Debug.Log("Spawning stopped.");
            }
        }

        public void PauseSpawning()
        {
            if (isSpawning && !isPaused)
            {
                isPaused = true;
                Debug.Log("Spawning paused.");
            }
        }

        public void ResumeSpawning()
        {
            if (isSpawning && isPaused)
            {
                isPaused = false;
                Debug.Log("Spawning resumed.");
            }
        }

        private IEnumerator SpawnWaves()
        {
            while (isSpawning) 
            {
                if (isPaused)
                {
                    yield return null;
                }

                yield return StartCoroutine(SpawnWaveEnemies(_waveData));
                _currentWaveIndex++;
                yield return new WaitForSeconds(_waveInterval);
            }
        }

        private IEnumerator SpawnWaveEnemies(List<WaveConfig> _waveData)
        {
            List<Coroutine> activeSpawnCoroutines = new List<Coroutine>();

            foreach (var waveConfig in _waveData)
            {
                Coroutine spawnCoroutine = StartCoroutine(SpawnEnemiesOfType(waveConfig));
                activeSpawnCoroutines.Add(spawnCoroutine);
            }

            foreach (var activeCoroutine in activeSpawnCoroutines)
            {
                yield return activeCoroutine;
            }
        }

        private IEnumerator SpawnEnemiesOfType(WaveConfig waveConfig)
        {
            for (int i = 0; i < waveConfig.spawnFrequencyPerWave; i++)
            {
                while (isPaused)
                {
                    yield return null;
                }

                if (!isSpawning) yield break;

                SpawnEnemy(waveConfig.enemy_SO.enemyID);
                yield return new WaitForSeconds(waveConfig.spawnInitialInterval);
            }
        }

        private void SpawnEnemy(int ID)
        {
            GameService.Instance.GetService<EnemyService>().SpawnEnemy(ID);
        }
    }

}
