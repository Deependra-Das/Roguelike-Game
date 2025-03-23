using UnityEngine;
using Roguelike.Main;

namespace Roguelike.Player
{
    public class PlayerService
    {
        private PlayerScriptableObject playerScriptableObject;
        private PlayerController playerController;

        public PlayerService(PlayerScriptableObject playerScriptableObject)
        {
            this.playerScriptableObject = playerScriptableObject;
            SubscribeToEvents();
        }

        private void SubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.AddListener(SpawnPlayer);

        private void UnsubscribeToEvents() => GameService.Instance.EventService.OnLevelSelected.RemoveListener(SpawnPlayer);

        public void SpawnPlayer(int levelId)
        {
            playerController = new PlayerController(playerScriptableObject);
            UnsubscribeToEvents();
        }

        public PlayerController GetPlayer() => playerController;
    }
}
