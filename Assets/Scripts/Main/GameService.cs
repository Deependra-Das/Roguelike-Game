using UnityEngine;
using System;
using System.Collections.Generic;
using Roguelike.Utilities;

namespace Roguelike.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
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

        }

        public void InjectDependencies()
        {

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

        public void InitializeService<T>(IService service, params object[] dependencies) where T : IService
        {
            Type serviceType = typeof(T);
            if (!_services.ContainsKey(serviceType))
            {
                Debug.LogWarning($"{serviceType} is already registered.");
            }
            else
            {
                service.Initialize(dependencies);
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
