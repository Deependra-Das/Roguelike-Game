using UnityEngine;
using System.Collections.Generic;
using Roguelike.Main;
using Roguelike.Utilities;
using Roguelike.Event;
using Roguelike.Level;

namespace Roguelike.Player
{
    public class PlayerService : IService
    {
        private List<PlayerScriptableObject> _playerScriptableObject;
        private PlayerController _playerController;
        private int _playerIDSelected = -1;
        private GameState _currentGameState;

        public PlayerService(List<PlayerScriptableObject> playerScriptableObject)
        {
            _playerScriptableObject = playerScriptableObject;
        }

        ~PlayerService() => UnsubscribeToEvents();

        public void Initialize(params object[] dependencies)
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
            EventService.Instance.OnCharacterSelected.AddListener(SelectPlayer);
            EventService.Instance.OnStartGameplay.AddListener(SpawnPlayer);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnCharacterSelected.RemoveListener(SelectPlayer);
            EventService.Instance.OnStartGameplay.RemoveListener(SpawnPlayer);
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void SelectPlayer(int playerId)
        {
            _playerIDSelected = playerId;
        }

        public void SpawnPlayer()
        {
            PlayerScriptableObject playerData = _playerScriptableObject.Find(playerSO => playerSO.ID == _playerIDSelected);
            _playerController = new PlayerController(playerData);
            GameService.Instance.SetCameraTarget(_playerController.PlayerGameObject);
        }

        public PlayerController GetPlayer() => _playerController;
    }
}
