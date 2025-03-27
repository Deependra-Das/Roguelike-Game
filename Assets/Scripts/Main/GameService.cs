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

namespace Roguelike.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [SerializeField] private CinemachineCamera _cinemachineCamera;
        [SerializeField] private UIService _uiService;

        [Header("Scriptable Objects")]
        [SerializeField] private List<LevelScriptableObject> _levelScriptableObjects;
        [SerializeField] private List<PlayerScriptableObject> _playerScriptableObjects;
        [SerializeField] private List<EnemyScriptableObject> _enemyScriptableObjects;


        private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            RegisterServices();
            InjectDependencies();
            Debug.Log("Game Service Running");
        }

        private void RegisterServices()
        {
            RegisterService<EventService>(new EventService());
            RegisterService<UIService>(_uiService);
            RegisterService<LevelService>(new LevelService(_levelScriptableObjects));
            RegisterService<PlayerService>(new PlayerService(_playerScriptableObjects));
            RegisterService<EnemyService>(new EnemyService(_enemyScriptableObjects));
        }

        public void InjectDependencies()
        {
            InitializeService<EventService>();
            InitializeService<UIService>();
            InitializeService<LevelService>();
            InitializeService<PlayerService>();
            InitializeService<EnemyService>();
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
    }
}
