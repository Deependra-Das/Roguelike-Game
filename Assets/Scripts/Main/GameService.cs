using UnityEngine;
using System;
using System.Collections.Generic;
using Roguelike.Utilities;
using Roguelike.Event;
using Roguelike.Level;
using Roguelike.Player;
using Roguelike.UI;

namespace Roguelike.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        [SerializeField] private UIService _uiService;

        [SerializeField] private List<LevelScriptableObject> _levelScriptableObjects;
        [SerializeField] private List<PlayerScriptableObject> _playerScriptableObjects;

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
            //GetService<EventService>().OnPlayerSelected.Invoke(2);
            //GetService<EventService>().OnLevelSelected.Invoke(1);
        }

        private void RegisterServices()
        {
            RegisterService<EventService>(new EventService());
            RegisterService<UIService>(_uiService);
            RegisterService<LevelService>(new LevelService(_levelScriptableObjects));
            RegisterService<PlayerService>(new PlayerService(_playerScriptableObjects));
        }

        public void InjectDependencies()
        {
            InitializeService<EventService>();
            InitializeService<UIService>();
            InitializeService<LevelService>();
            InitializeService<PlayerService>();            
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
    }
}
