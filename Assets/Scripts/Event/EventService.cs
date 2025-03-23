using System;
using UnityEngine;
using Roguelike.Utilities;

namespace Roguelike.Event
{
    public class EventService : IService
    {
        public EventController<Action<int>> OnLevelSelected { get; private set; }

        public EventService()
        {
            OnLevelSelected = new EventController<Action<int>>();
        }

        public void Initialize(params object[] dependencies) 
        {
            Debug.Log("Event Service Running");
        }
    }
}
