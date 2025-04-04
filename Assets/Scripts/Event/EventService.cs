using System;
using UnityEngine;
using System.Collections.Generic;
using Roguelike.Utilities;
using Roguelike.UI;
using Roguelike.Wave;
using Roguelike.Main;
using Roguelike.Weapon;

namespace Roguelike.Event
{
    public class EventService : GenericMonoSingleton<EventService>
    {
        public EventController<Action<int>> OnCharacterSelected { get; private set; }
        public EventController<Action<int>> OnLevelSelected { get; private set; }
        public EventController<Action> OnQuitGameButtonClicked { get; private set; }
        public EventController<Action> OnContinueButtonClicked { get; private set; }
        public EventController<Action> OnBackToMainMenuButtonClicked { get; private set; }
        public EventController<Action> OnGiveUpButtonClicked { get; private set; }
        public EventController<Action> OnMainMenu { get; private set; }
        public EventController<Action> OnLevelSelection { get; private set; }
        public EventController<Action> OnCharacterSelection { get; private set; }
        public EventController<Action> OnPowerUpSelection { get; private set; }
        public EventController<Action> OnStartGameplay { get; private set; }
        public EventController<Action> OnGameplay { get; private set; }
        public EventController<Action> OnGamePaused { get; private set; }
        public EventController<Action> OnGameOver { get; private set; }
        public EventController<Action> OnLevelCompleted { get; private set; }
        public EventController<Action> OnSaveHighScore { get; private set; }
        public EventController<Action<List<WeaponController>>> OnWeaponAdded { get; private set; }
        public EventController<Action<float,float,float,List<WaveConfig>>> OnStartWaveSpawn { get; private set; }
        public EventController<Action<GameState>> OnGameStateChange { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
        }

        public void Initialize()
        {
            OnCharacterSelected = new EventController<Action<int>>();
            OnLevelSelected = new EventController<Action<int>>();

            OnQuitGameButtonClicked = new EventController<Action>();
            OnContinueButtonClicked = new EventController<Action>();
            OnBackToMainMenuButtonClicked = new EventController<Action>();
            OnGiveUpButtonClicked = new EventController<Action>();

            OnGameStateChange = new EventController<Action<GameState>>();
            OnMainMenu = new EventController<Action>();
            OnLevelSelection = new EventController<Action>();
            OnCharacterSelection = new EventController<Action>();
            OnPowerUpSelection = new EventController<Action>();
            OnStartGameplay = new EventController<Action>();
            OnGameplay = new EventController<Action>();
            OnGamePaused = new EventController<Action>();
            OnGameOver = new EventController<Action>();
            OnLevelCompleted = new EventController<Action>();
            OnSaveHighScore = new EventController<Action>();
            OnWeaponAdded = new EventController<Action<List<WeaponController>>>();
            OnStartWaveSpawn = new EventController<Action<float,float,float, List<WaveConfig>>>();
        }

    }
}
