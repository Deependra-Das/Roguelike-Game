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

        public PlayerService(List<PlayerScriptableObject> playerScriptableObject)
        {
            _playerScriptableObject = playerScriptableObject;
        }

        public void Initialize(params object[] dependencies)
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnCharacterSelected.AddListener(SelectPlayer);
            GameService.Instance.GetService<EventService>().OnStartGame.AddListener(SpawnPlayer);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnCharacterSelected.RemoveListener(SelectPlayer);
            GameService.Instance.GetService<EventService>().OnStartGame.RemoveListener(SpawnPlayer);
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
            UnsubscribeToEvents();
        }

        public PlayerController GetPlayer() => _playerController;
    }
}
