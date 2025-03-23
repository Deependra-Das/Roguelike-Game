using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerController
    {
        private PlayerScriptableObject _playerScriptableObject;
        private PlayerView _playerView;

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
        }

        public void UpdatePlayer() { }       

        public void FixedUpdatePlayer() { }

        public void TakeDamage(int damageTaken) { }
    }

}
