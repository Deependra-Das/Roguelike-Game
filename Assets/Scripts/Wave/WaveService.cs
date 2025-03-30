using Roguelike.Utilities;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roguelike.Enemy;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Level;

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
        private GameState _currentGameState;

        public void Initialize(params object[] dependencies)
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
            EventService.Instance.OnStartGameplay.AddListener(StartWave);
            EventService.Instance.OnGamePaused.AddListener(PauseSpawning);
            EventService.Instance.OnGameplay.AddListener(ResumeSpawning);
            EventService.Instance.OnGameOver.AddListener(StopSpawning);
            EventService.Instance.OnLevelCompleted.AddListener(StopSpawning);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnStartGameplay.RemoveListener(StartWave);
            EventService.Instance.OnGamePaused.RemoveListener(PauseSpawning);
            EventService.Instance.OnGameplay.RemoveListener(ResumeSpawning);
            EventService.Instance.OnGameOver.RemoveListener(StopSpawning);
            EventService.Instance.OnLevelCompleted.RemoveListener(StopSpawning);
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        private void StartWave()
        {
            LevelScriptableObject levelData = GameService.Instance.GetService<LevelService>().GetLevelData();
            if (levelData.enemyWaveData == null || levelData.enemyWaveData.Count == 0)
            {
                Debug.LogError("No wave data available to spawn.");
                return;
            }

            _currentWaveIndex = 1;
            _spawnInitialIntervalDecrementRate = levelData.spawnIntervalDecrementRate;
            _spawnFinalInterval = levelData.spawnFinalInterval;
            _waveInterval = levelData.waveInterval;

            InitializeWaveData(levelData.enemyWaveData);

            isSpawning = true;
            isPaused = false;
            spawnCoroutine = StartCoroutine(SpawnWaves());
        }

        private void InitializeWaveData(List<WaveConfig> waveData)
        {
            _waveData.Clear();
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
            isSpawning = false;
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
                Debug.Log("Spawning stopped.");
            }
        }

        public void PauseSpawning()
        {
            isPaused = true;
            Debug.Log("Spawning paused.");
        }

        public void ResumeSpawning()
        {
            isPaused = false;
            Debug.Log("Spawning resumed.");
        }

        private IEnumerator SpawnWaves()
        {
            while(isSpawning)
            {
                if (!isSpawning) yield break;

                foreach (WaveConfig waveConfig in _waveData)
                {
                    if (!isSpawning) yield break;
                    if (isPaused) yield return new WaitUntil(() => !isPaused);

                    for (int i = 0; i < waveConfig.spawnFrequencyPerWave; i++)
                    {
                        if (!isSpawning) yield break;
                        if (isPaused) yield return new WaitUntil(() => !isPaused);

                        SpawnEnemy(waveConfig.enemy_SO.enemyID);
                        yield return new WaitForSeconds(waveConfig.spawnInitialInterval);
                    }
                }

                if (!isSpawning) yield break;
                yield return new WaitForSeconds(_waveInterval);
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
