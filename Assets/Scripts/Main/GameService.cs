using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using Unity.Cinemachine;
using Roguelike.Utilities;
using Roguelike.Event;
using Roguelike.Level;
using Roguelike.Player;
using Roguelike.Enemy;
using Roguelike.UI;
using Roguelike.Wave;
using Roguelike.Weapon;
using Roguelike.Projectile;
using Roguelike.VFX;
using Roguelike.DamageNumber;
using Roguelike.Sound;
using Roguelike.HighScore;
using Roguelike.Camera;

namespace Roguelike.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        #region Inspector-Dependencies
        [SerializeField] private CinemachineCamera _cinemachineCamera;
        [SerializeField] private int _waitTimeBeforeInitialExp = 0;

        [Header("Service References")]
        [SerializeField] private WaveService _waveService;

        [Header("Prefabs")]
        [SerializeField] private GameObject _smokeVFXPrefab;
        [SerializeField] private GameObject _dmgNumPrefab;

        [Header("Audio Sources")]
        [SerializeField] private List<AudioSource> _audioSourceList;

        [Header("Scriptable Objects")]
        [SerializeField] private LevelScriptableObject _levelScriptableObject;
        [SerializeField] private PlayerScriptableObject _playerScriptableObject;
        [SerializeField] private List<EnemyScriptableObject> _enemyScriptableObjects;
        [SerializeField] private List<WeaponScriptableObject> _weaponScriptableObjects;
        [SerializeField] private List<ProjectileScriptableObject> _projectileScriptableObjects;
        [SerializeField] private SoundScriptableObject _audioList;
        [SerializeField] private UI_Data_ScriptableObject _uiDataScriptableObject;
        #endregion

        private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();
        public GameState GameState { get; private set; }
        public float GameTimer { get; private set; }

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            EventService.Instance.Initialize();
            RegisterServices();
            InitializeServices();
            ChangeGameState(GameState.MainMenu);
        }

        private void RegisterServices()
        {
            ServiceLocator.Instance.RegisterService<UIService>(new UIService(_uiDataScriptableObject, _levelScriptableObject, _playerScriptableObject));
            ServiceLocator.Instance.RegisterService<DamageNumberService>(new DamageNumberService(_dmgNumPrefab));
            ServiceLocator.Instance.RegisterService<SoundService>(new SoundService(_audioList, _audioSourceList));
            ServiceLocator.Instance.RegisterService<CameraService>(new CameraService(_cinemachineCamera));
            ServiceLocator.Instance.RegisterService<HighScoreService>(new HighScoreService());
            ServiceLocator.Instance.RegisterService<LevelService>(new LevelService(_levelScriptableObject));
            ServiceLocator.Instance.RegisterService<PlayerService>(new PlayerService(_playerScriptableObject));
            ServiceLocator.Instance.RegisterService<ProjectileService>(new ProjectileService(_projectileScriptableObjects));
            ServiceLocator.Instance.RegisterService<WeaponService>(new WeaponService(_weaponScriptableObjects));
            ServiceLocator.Instance.RegisterService<EnemyService>(new EnemyService(_enemyScriptableObjects));
            ServiceLocator.Instance.RegisterService<WaveService>(_waveService);
            ServiceLocator.Instance.RegisterService<VFXService>(new VFXService(_smokeVFXPrefab));                
        }

        public void InitializeServices()
        {
            ServiceLocator.Instance.InitializeService<UIService>();
            ServiceLocator.Instance.InitializeService<DamageNumberService>();
            ServiceLocator.Instance.InitializeService<CameraService>();
            ServiceLocator.Instance.InitializeService<SoundService>();
            ServiceLocator.Instance.InitializeService<HighScoreService>();
            ServiceLocator.Instance.InitializeService<LevelService>();
            ServiceLocator.Instance.InitializeService<PlayerService>();
            ServiceLocator.Instance.InitializeService<ProjectileService>();
            ServiceLocator.Instance.InitializeService<WeaponService>();
            ServiceLocator.Instance.InitializeService<EnemyService>();
            ServiceLocator.Instance.InitializeService<WaveService>();
            ServiceLocator.Instance.InitializeService<VFXService>();
        }

        void Update()
        {
            if (GameState == GameState.Gameplay)
            {
                GameTimer += Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ChangeGameState(GameState.GamePaused);
                }
            }
        }

        public void ChangeGameState(GameState newState)
        {
            GameState = newState;
            EventService.Instance.OnGameStateChange.Invoke(GameState);

            switch (newState)
            {
                case GameState.MainMenu:
                    Time.timeScale = 1;
                    EventService.Instance.OnMainMenu.Invoke();
                    break;
                case GameState.LevelSelection:
                    EventService.Instance.OnLevelSelection.Invoke();
                    break;
                case GameState.CharacterSelection:
                    EventService.Instance.OnCharacterSelection.Invoke();
                    break;
                case GameState.PowerUpSelection:
                    EventService.Instance.OnPowerUpSelection.Invoke();
                    break;
                case GameState.Gameplay:
                    EventService.Instance.OnGameplay.Invoke();
                    break;
                case GameState.GamePaused:
                    EventService.Instance.OnGamePaused.Invoke();
                    break;
                case GameState.LevelCompleted:
                    Time.timeScale = 0;
                    EventService.Instance.OnSaveHighScore.Invoke();
                    EventService.Instance.OnLevelCompleted.Invoke();
                    break;
                case GameState.GameOver:
                    Time.timeScale = 0;
                    EventService.Instance.OnSaveHighScore.Invoke();
                    EventService.Instance.OnGameOver.Invoke();
                    break;
            }
        }

        public void StartGameplay()
        {
            EventService.Instance.OnStartGameplay.Invoke();
            ChangeGameState(GameState.Gameplay);
            ServiceLocator.Instance.GetService<CameraService>().ResetCameraPosition();
            ResetGameTimer();
            StartCoroutine(GiveIntialSpawnExpPoints(_waitTimeBeforeInitialExp));
        }

        public IEnumerator GiveIntialSpawnExpPoints(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            if(GameState==GameState.Gameplay)
            {
                ServiceLocator.Instance.GetService<PlayerService>().GetPlayer().AddExperiencePoints(1);
            }            
        }

        private void ResetGameTimer()
        {
            GameTimer = 0;
        }
    }
}
