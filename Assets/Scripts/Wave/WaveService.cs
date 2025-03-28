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
        [SerializeField] private Transform minPos;
        [SerializeField] private Transform maxPos;
        private List<WaveConfig> _waveData = new List<WaveConfig>();
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
            GameService.Instance.GetService<EventService>().OnGameOver.AddListener(StopSpawning);
            GameService.Instance.GetService<EventService>().OnLevelCompleted.AddListener(StopSpawning);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnStartWaveSpawn.RemoveListener(StartWave);
            GameService.Instance.GetService<EventService>().OnPauseGame.RemoveListener(PauseSpawning);
            GameService.Instance.GetService<EventService>().OnContinueButtonClicked.RemoveListener(ResumeSpawning);
            GameService.Instance.GetService<EventService>().OnGameOver.RemoveListener(StopSpawning);
            GameService.Instance.GetService<EventService>().OnLevelCompleted.RemoveListener(StopSpawning);
        }

        private void StartWave(float spawnInitialIntervalDecrementRate, float spawnFinalInterval, float waveInterval, List<WaveConfig> waveData)
        {
            _currentWaveIndex = 1;
            _spawnInitialIntervalDecrementRate = spawnInitialIntervalDecrementRate;
            _spawnFinalInterval = spawnFinalInterval;
            _waveInterval = waveInterval;

            InitializeWaveData(waveData);

            if (!isSpawning && !isPaused)
            {
                isSpawning = true;
                spawnCoroutine = StartCoroutine(SpawnWaves());
            }
        }

        private void InitializeWaveData(List<WaveConfig> waveData)
        {
            foreach (WaveConfig waveConfig in waveData)
            {
                WaveConfig config = new WaveConfig();
                config.enemy_SO = waveConfig.enemy_SO;
                config.spawnFrequencyPerWave = waveConfig.spawnFrequencyPerWave;
                config.spawnInitialInterval = waveConfig.spawnInitialInterval;
                _waveData.Add(config);
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

                Debug.Log("Wave :" + _currentWaveIndex.ToString());
                yield return StartCoroutine(SpawnWaveEnemies(_waveData));
                _currentWaveIndex++;
                foreach (var wave in _waveData)
                {
                    if (wave.spawnInitialInterval > _spawnFinalInterval)
                    {
                        wave.spawnInitialInterval *= _spawnInitialIntervalDecrementRate;
                    }
                }

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

                yield return new WaitForSeconds(waveConfig.spawnInitialInterval);
                SpawnEnemy(waveConfig.enemy_SO.enemyID);
               
            }
        }

        private void SpawnEnemy(int ID)
        {
            GameService.Instance.GetService<EnemyService>().SpawnEnemy(ID, RandomSpawnPoint());
        }
        private Vector2 RandomSpawnPoint()
        {
            Vector2 spawnPoint;
            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);
                if (Random.Range(0f, 1f) > 0.5f)
                {
                    spawnPoint.y = minPos.position.y;
                }
                else
                {
                    spawnPoint.y = maxPos.position.y;
                }
            }
            else
            {
                spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);
                if (Random.Range(0f, 1f) > 0.5f)
                {
                    spawnPoint.x = minPos.position.x;
                }
                else
                {
                    spawnPoint.x = maxPos.position.x;
                }
            }


            return spawnPoint;
        }

    }

}
