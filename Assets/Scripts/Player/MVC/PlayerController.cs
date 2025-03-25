using Roguelike.Main;
using Roguelike.Event;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerController
    {
        private PlayerScriptableObject _playerScriptableObject;
        private PlayerView _playerView;
        private bool isPaused = false;

        public PlayerController(PlayerScriptableObject playerScriptableObject)
        {
            _playerScriptableObject = playerScriptableObject;
            InitializeView();
        }

        private void InitializeView()
        {
            _playerView = Object.Instantiate(_playerScriptableObject.playerPrefab);
            _playerView.transform.position = _playerScriptableObject.spawnPosition;
            _playerView.transform.rotation = Quaternion.Euler(_playerScriptableObject.spawnRotation);
            _playerView.SetController(this);
            SubscribeToEvents();
        }
        private void SubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnContinueButtonClicked.AddListener(ContinueGame);
        }

        private void UnsubscribeToEvents()
        {
            GameService.Instance.GetService<EventService>().OnContinueButtonClicked.RemoveListener(ContinueGame);
        }

        public void UpdatePlayer() 
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
            {
                PauseGame();
            }
        }       

        public void FixedUpdatePlayer() { }

        public void TakeDamage(int damageTaken) { }

        private void PauseGame()
        {
            isPaused = true;
            GameService.Instance.GetService<EventService>().OnPauseGame.Invoke();
            Debug.Log("Game Paused");
        }

        private void ContinueGame()
        {
            isPaused = false;
            Debug.Log("Game Resumed");
        }

        public void OnDestroy() => UnsubscribeToEvents();

    }

}
