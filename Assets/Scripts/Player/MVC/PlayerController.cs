using Roguelike.Main;
using Roguelike.Event;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerController
    {
        private PlayerScriptableObject _playerScriptableObject;
        private PlayerModel _playerModel;
        private PlayerView _playerView;
        private bool isPaused = false;

        public PlayerController(PlayerScriptableObject playerScriptableObject)
        {
            _playerScriptableObject = playerScriptableObject;
            InitializeController();
        }

        private void InitializeController()
        {
            InitializeModel();
            InitializeView();
            SubscribeToEvents();
        }

        private void InitializeModel()
        {
            _playerModel = new PlayerModel(_playerScriptableObject);
        }

        private void InitializeView()
        {
            _playerView = Object.Instantiate(_playerModel.PlayerPrefab);
            _playerView.transform.position = _playerModel.SpawnPosition;
            _playerView.transform.rotation = Quaternion.Euler(_playerModel.SpawnRotation);
            _playerView.SetController(this);           
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

        public PlayerModel PlayerModel { get { return _playerModel; } }

        public void OnDestroy() => UnsubscribeToEvents();

    }

}
