using System;
using UnityEngine;
using Roguelike.Utilities;
using Roguelike.UI;

namespace Roguelike.Event
{
    public class EventService : IService
    {
        public EventController<Action<int>> OnPlayerSelected { get; private set; }
        public EventController<Action<int>> OnLevelSelected { get; private set; }
        public EventController<Action> OnNewGameButtonSelected { get; private set; }
        public EventController<Action> OnQuitGameButtonSelected { get; private set; }
        public EventController<Action> OnStartGame { get; private set; }

        public EventService()
        {
            OnPlayerSelected = new EventController<Action<int>>();
            OnLevelSelected = new EventController<Action<int>>();
            OnNewGameButtonSelected = new EventController<Action>();
            OnQuitGameButtonSelected = new EventController<Action>();
            OnStartGame = new EventController<Action>();
        }

        public void Initialize(params object[] dependencies) 
        {
            Debug.Log("Event Service Running");
        }
    }
}
