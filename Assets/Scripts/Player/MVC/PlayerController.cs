using Roguelike.Main;
using Roguelike.Event;
using Roguelike.Enemy;
using Roguelike.Level;
using UnityEngine;
using System.Collections.Generic;
using Roguelike.UI;
using Roguelike.Weapon;

namespace Roguelike.Player
{
    public class PlayerController
    {
        private PlayerScriptableObject _playerScriptableObject;
        private PlayerModel _playerModel;
        private PlayerView _playerView;
        protected bool isDead;
        protected List<int> expToUpgradeList;
        private GameState _currentGameState;
        private List<WeaponController> _weapons = new List<WeaponController>();

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
            UpdateInitialHealthOnUI();
            AddWeapon();
        }

        private void UpdateInitialHealthOnUI()
        {
            expToUpgradeList = GameService.Instance.GetService<LevelService>().GetExpToUpgradeList();
            GameService.Instance.GetService<UIService>().UpdateMaxHealthSlider(_playerModel.MaxHealth);
            GameService.Instance.GetService<UIService>().UpdateCurrentHealthSlider(_playerModel.CurrentHealth);
            GameService.Instance.GetService<UIService>().UpdateMaxExpSlider(expToUpgradeList[_playerModel.CurrentExpLevel]);
            GameService.Instance.GetService<UIService>().UpdateCurrentExpSlider(_playerModel.CurrentExpPoints);
        }

        private void AddWeapon()
        {
            _weapons.Clear();
            _weapons.Add(GameService.Instance.GetService<WeaponService>().CreateWeapons(WeaponType.RadialReap,_playerView.playerWeaponTransform));
            _weapons.Add(GameService.Instance.GetService<WeaponService>().CreateWeapons(WeaponType.OrbitalFury, _playerView.playerWeaponTransform));
            _weapons.Add(GameService.Instance.GetService<WeaponService>().CreateWeapons(WeaponType.ScatterShot, _playerView.playerWeaponTransform));
            EventService.Instance.OnWeaponAdded.Invoke(_weapons);
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
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
            EventService.Instance.OnGameOver.AddListener(OnGameOver);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnGameOver.RemoveListener(OnGameOver);
        }

        public void UpdatePlayer() 
        {
            if (isDead || _currentGameState!=GameState.Gameplay) return;
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
            _playerView.SetGameState(_currentGameState);
            Debug.Log(_currentGameState.ToString());
        }

        public PlayerModel PlayerModel { get { return _playerModel; } }

        public GameObject PlayerGameObject { get { return _playerView.gameObject; } }

        public void OnDestroy() => UnsubscribeToEvents();

        public void TakeDamage(int damage)
        {
            if (!isDead && _currentGameState == GameState.Gameplay)
            {
                _playerModel.UpdateCurrentHealth(-damage);
                GameService.Instance.GetService<UIService>().UpdateCurrentHealthSlider(_playerModel.CurrentHealth);
                if (_playerModel.CurrentHealth <= 0)
                {
                    isDead = true;
                    OnPlayerDeath();
                }
            }
        }

        private void OnPlayerDeath()
        {
            GameService.Instance.ChangeGameState(GameState.GameOver);
            _playerView.OnPlayerDeath();
        }

        private void OnGameOver()
        {
            if(!isDead)
            {
                _playerModel.UpdateCurrentHealth(_playerModel.CurrentHealth);
                isDead = true;
                _playerView.OnPlayerDeath();
            }
        }

        public void AddExperiencePoints(int value)
        {
            _playerModel.UpdateExperiencePoints(value);
            GameService.Instance.GetService<UIService>().UpdateCurrentExpSlider(_playerModel.CurrentExpPoints);
            if(_playerModel.CurrentExpPoints>= expToUpgradeList[_playerModel.CurrentExpLevel])
            {
                ExpLevelUp();
            }
        }

        public void ExpLevelUp()
        {
            int valueToDeduct = expToUpgradeList[_playerModel.CurrentExpLevel];
            _playerModel.UpdateExperiencePoints(-(valueToDeduct));
            GameService.Instance.GetService<UIService>().UpdateCurrentExpSlider(_playerModel.CurrentExpPoints);
            _playerModel.UpdateExpLevel();
            GameService.Instance.GetService<UIService>().UpdateMaxExpSlider(expToUpgradeList[_playerModel.CurrentExpLevel]);
            GameService.Instance.ChangeGameState(GameState.PowerUpSelection);
        }

        public void Heal()
        {
            int amountToHeal = _playerModel.MaxHealth - _playerModel.CurrentHealth;
            _playerModel.UpdateCurrentHealth(amountToHeal);
            GameService.Instance.GetService<UIService>().UpdateCurrentHealthSlider(_playerModel.CurrentHealth);
        }

        public void UpgradeMaxHealth(int value)
        {
            _playerModel.UpdateMaxHealth(value);
            GameService.Instance.GetService<UIService>().UpdateMaxHealthSlider(_playerModel.MaxHealth);
        }
    }

}
