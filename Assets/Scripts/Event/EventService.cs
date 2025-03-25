using System;
using UnityEngine;
using Roguelike.Utilities;
using Roguelike.UI;

namespace Roguelike.Event
{
    public class EventService : IService
    {
        public EventController<Action<int>> OnCharacterSelected { get; private set; }
        public EventController<Action<int>> OnLevelSelected { get; private set; }
        public EventController<Action> OnNewGameButtonClicked { get; private set; }
        public EventController<Action> OnQuitGameButtonClicked { get; private set; }
        public EventController<Action> OnStartGame { get; private set; }
        public EventController<Action> OnPauseGame { get; private set; }
        public EventController<Action> OnContinueButtonClicked { get; private set; }
        public EventController<Action> OnBackToMainMenuButtonClicked { get; private set; }
        public EventController<Action> OnGiveUpButtonClicked { get; private set; }
        public EventController<Action> OnGameOver { get; private set; }

        public EventService()
        {
            OnCharacterSelected = new EventController<Action<int>>();
            OnLevelSelected = new EventController<Action<int>>();
            OnNewGameButtonClicked = new EventController<Action>();
            OnQuitGameButtonClicked = new EventController<Action>();
            OnStartGame = new EventController<Action>();
            OnPauseGame = new EventController<Action>();
            OnContinueButtonClicked = new EventController<Action>();
            OnBackToMainMenuButtonClicked = new EventController<Action>();
            OnGiveUpButtonClicked = new EventController<Action>();
            OnGameOver = new EventController<Action>();
        }

        public void Initialize(params object[] dependencies) 
        {
            Debug.Log("Event Service Running");
        }
    }
}
