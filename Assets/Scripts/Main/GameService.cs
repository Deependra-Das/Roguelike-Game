using UnityEngine;
using System;
using System.Collections.Generic;
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

namespace Roguelike.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [SerializeField] private CinemachineCamera _cinemachineCamera;
        [SerializeField] private UIService _uiService;
        [SerializeField] private WaveService _waveService;

        [Header("Scriptable Objects")]
        [SerializeField] private List<LevelScriptableObject> _levelScriptableObjects;
        [SerializeField] private List<PlayerScriptableObject> _playerScriptableObjects;
        [SerializeField] private List<EnemyScriptableObject> _enemyScriptableObjects;
        [SerializeField] private List<WeaponScriptableObject> _weaponScriptableObjects;
        [SerializeField] private List<ProjectileScriptableObject> _projectileScriptableObjects;

        private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();
        public GameState GameState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            RegisterServices();
            InjectDependencies();
            ChangeGameState(GameState.MainMenu);
        }

        private void RegisterServices()
        {
            RegisterService<UIService>(_uiService);
            RegisterService<LevelService>(new LevelService(_levelScriptableObjects));
            RegisterService<PlayerService>(new PlayerService(_playerScriptableObjects));
            RegisterService<ProjectileService>(new ProjectileService(_projectileScriptableObjects));
            RegisterService<WeaponService>(new WeaponService(_weaponScriptableObjects));
            RegisterService<EnemyService>(new EnemyService(_enemyScriptableObjects));
            RegisterService<WaveService>(_waveService);
        }

        public void InjectDependencies()
        {
            EventService.Instance.Initialize();
            InitializeService<UIService>();
            InitializeService<LevelService>();
            InitializeService<PlayerService>();
            InitializeService<ProjectileService>();
            InitializeService<WeaponService>();
            InitializeService<EnemyService>();
            InitializeService<WaveService>();
        }

        public void RegisterService<T>(IService service) where T : IService
        {
            Type serviceType = typeof(T);
            if (!_services.ContainsKey(serviceType))
            {
                _services[serviceType] = service;
            }
            else
            {
                Debug.LogWarning($"{serviceType} is already registered.");
            }
        }

        public void InitializeService<T>(params object[] dependencies) where T : IService
        {
            Type serviceType = typeof(T);
            if (_services.ContainsKey(serviceType))
            {
                IService service = _services[serviceType];
                service.Initialize(dependencies);
            }
            else
            {
                Debug.LogWarning($"Service {serviceType} is not registered.");
            }
        }

        public T GetService<T>() where T : IService
        {
            Type serviceType = typeof(T);
            if (_services.ContainsKey(serviceType))
            {
                return (T)_services[serviceType];
            }
            else
            {
                Debug.LogError($"{serviceType} is not registered.");
                return default;
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && GameState==GameState.Gameplay)
            {
                ChangeGameState(GameState.GamePaused);
            }
        }

        public void SetCameraTarget(GameObject targetGameObject)
        {
            if (_cinemachineCamera != null && targetGameObject != null)
            {
                _cinemachineCamera.Follow = targetGameObject.transform;
            }
            else
            {
                Debug.LogWarning("Cinemachine Virtual Camera or Target GameObject is not assigned.");
            }

        }

        public void ChangeGameState(GameState newState)
        {
            GameState = newState;
            EventService.Instance.OnGameStateChange.Invoke(GameState);

            switch (newState)
            {
                case GameState.MainMenu:
                    EventService.Instance.OnMainMenu.Invoke();
                    break;
                case GameState.LevelSelection:
                    EventService.Instance.OnLevelSelection.Invoke();
                    break;
                case GameState.CharacterSelection:
                    EventService.Instance.OnCharacterSelection.Invoke();
                    break;
                case GameState.PowerUpSelection:
                    //GetService<EventService>().OnPowerUpSelection.Invoke();
                    break;
                case GameState.Gameplay:
                    EventService.Instance.OnGameplay.Invoke();
                    break;
                case GameState.GamePaused:
                    EventService.Instance.OnGamePaused.Invoke();
                    break;
                case GameState.LevelCompleted:
                    EventService.Instance.OnLevelCompleted.Invoke();
                    break;
                case GameState.GameOver:
                    EventService.Instance.OnGameOver.Invoke();
                    break;
            }
        }

        public void StartGameplay()
        {
            EventService.Instance.OnStartGameplay.Invoke();
            ChangeGameState(GameState.Gameplay);
        }
    }
}
