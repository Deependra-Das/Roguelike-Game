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
        private int _playerIDSelected;

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
            GameService.Instance.GetService<EventService>().OnPlayerSelected.AddListener(SelectPlayer);
            GameService.Instance.GetService<EventService>().OnLevelSelected.AddListener(SpawnPlayer);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnPlayerSelected.RemoveListener(SelectPlayer);
            GameService.Instance.GetService<EventService>().OnLevelSelected.RemoveListener(SpawnPlayer);
        }

        public void SelectPlayer(int playerId)
        {
            _playerIDSelected = playerId;
        }

        public void SpawnPlayer(int levelId)
        {
            PlayerScriptableObject playerData = _playerScriptableObject.Find(playerSO => playerSO.ID == _playerIDSelected);
            _playerController = new PlayerController(playerData);
            UnsubscribeToEvents();
        }

        public PlayerController GetPlayer() => _playerController;
    }
}
