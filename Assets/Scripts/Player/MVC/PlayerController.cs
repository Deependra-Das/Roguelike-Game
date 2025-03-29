using Roguelike.Main;
using Roguelike.Event;
using UnityEngine;
using Roguelike.Enemy;

namespace Roguelike.Player
{
    public class PlayerController
    {
        private PlayerScriptableObject _playerScriptableObject;
        private PlayerModel _playerModel;
        private PlayerView _playerView;
        private bool isPaused = false;
        protected bool isDead;

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
            if (isDead) return;

            if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && !isDead)
            {
                PauseGame();
            }
        }       

        public void FixedUpdatePlayer() { }

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

        public GameObject PlayerGameObject { get { return _playerView.gameObject; } }

        public void OnDestroy() => UnsubscribeToEvents();

        public void TakeDamage(int damage)
        {
            _playerModel.UpdateCurrentHealth(-damage);
            if (_playerModel.CurrentHealth <= 0)
            {
                isDead = true;
                OnEnemyDeath();
            }
        }

        public void OnEnemyDeath()
        {
            GameService.Instance.GetService<EventService>().OnGameOver.Invoke();
            _playerView.OnEnemyDeath();
        }
    }

}
