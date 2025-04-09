using UnityEngine;
using System.Collections.Generic;
using Roguelike.Main;
using Roguelike.Utilities;
using Roguelike.Event;
using Roguelike.Level;
using Roguelike.Camera;

namespace Roguelike.Player
{
    public class PlayerService : IService
    {
        private Dictionary<int, PlayerData> _playerDataDictionary;
        private PlayerController _playerController;
        private int _playerIDSelected = -1;
        private GameState _currentGameState;

        public PlayerService(PlayerScriptableObject playerScriptableObject)
        {
            ConvertPlayerDataListToDictionary(playerScriptableObject.playerDataList);
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
            EventService.Instance.OnGameOver.AddListener(OnGameOver);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnCharacterSelected.RemoveListener(SelectPlayer);
            EventService.Instance.OnStartGameplay.RemoveListener(SpawnPlayer);
            EventService.Instance.OnGameOver.RemoveListener(OnGameOver);
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
            if (_playerDataDictionary.ContainsKey(_playerIDSelected))
            {
                PlayerData playerData = _playerDataDictionary[_playerIDSelected];
                _playerController = new PlayerController(playerData);
                ServiceLocator.Instance.GetService<CameraService>().SetCameraTarget(_playerController.PlayerGameObject);
            }
            else
            {
                Debug.Log("Player with the specified ID not found.");
            }
        }

        public PlayerController GetPlayer() => _playerController;

        private void OnGameOver()
        {
            _playerController = null;
        }

        private void ConvertPlayerDataListToDictionary(List<PlayerData> playerDataList)
        {
            _playerDataDictionary = new Dictionary<int, PlayerData>();

            foreach (var player in playerDataList)
            {
                _playerDataDictionary.Add(player.ID, player);
            }
        }
    }
}
