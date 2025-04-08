using Roguelike.Main;
using Roguelike.Event;
using Roguelike.Enemy;
using Roguelike.Level;
using UnityEngine;
using System.Collections.Generic;
using Roguelike.UI;
using Roguelike.Weapon;
using Roguelike.Sound;

namespace Roguelike.Player
{
    public class PlayerController
    {
        private PlayerScriptableObject _playerScriptableObject;
        private PlayerModel _playerModel;
        private PlayerView _playerView;
        private GameState _currentGameState;
        private List<WeaponController> _weapons = new List<WeaponController>();
        protected bool isDead;
        protected List<int> expToUpgradeList;

        public PlayerController(PlayerScriptableObject playerScriptableObject)
        {
            _playerScriptableObject = playerScriptableObject;
            InitializeController();
        }

        ~PlayerController() => UnsubscribeToEvents();

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
            UIService.Instance.UpdateMaxHealthSlider(_playerModel.MaxHealth);
            UIService.Instance.UpdateCurrentHealthSlider(_playerModel.CurrentHealth);
            UIService.Instance.UpdateMaxExpSlider(expToUpgradeList[_playerModel.CurrentExpLevel]);
            UIService.Instance.UpdateCurrentExpSlider(_playerModel.CurrentExpPoints);
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
            EventService.Instance.OnLevelCompleted.AddListener(OnGameOver);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
            EventService.Instance.OnGameOver.RemoveListener(OnGameOver);
            EventService.Instance.OnLevelCompleted.AddListener(OnGameOver);
        }

        public void UpdatePlayer() 
        {
            if (isDead || _currentGameState!=GameState.Gameplay) return;
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
            _playerView.SetGameState(_currentGameState);
        }

        public PlayerModel PlayerModel { get { return _playerModel; } }

        public GameObject PlayerGameObject { get { return _playerView.gameObject; } }

        public void TakeDamage(int damage)
        {
            if (!isDead && _currentGameState == GameState.Gameplay)
            {
                _playerModel.UpdateCurrentHealth(-damage);
                UIService.Instance.UpdateCurrentHealthSlider(_playerModel.CurrentHealth);
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
            UIService.Instance.UpdateCurrentExpSlider(_playerModel.CurrentExpPoints);

            if(_playerModel.CurrentExpPoints>= expToUpgradeList[_playerModel.CurrentExpLevel])
            {
                if(!CheckMaxExpLevelReached())
                {
                    ExpLevelUp();
                }
                else
                {
                    GameService.Instance.ChangeGameState(GameState.LevelCompleted);
                }
            
            }
        }

        public void ExpLevelUp()
        {
            int valueToDeduct = expToUpgradeList[_playerModel.CurrentExpLevel];
            _playerModel.UpdateExperiencePoints(-(valueToDeduct));
            UIService.Instance.UpdateCurrentExpSlider(_playerModel.CurrentExpPoints);
            _playerModel.UpdateExpLevel();
            UIService.Instance.UpdateMaxExpSlider(expToUpgradeList[_playerModel.CurrentExpLevel]);
            GameService.Instance.GetService<SoundService>().PlaySFX(SoundType.LevelUp);
            GameService.Instance.ChangeGameState(GameState.PowerUpSelection);
        }

        public void Heal()
        {
            int amountToHeal = _playerModel.MaxHealth - _playerModel.CurrentHealth;
            _playerModel.UpdateCurrentHealth(amountToHeal);
            UIService.Instance.UpdateCurrentHealthSlider(_playerModel.CurrentHealth);
        }

        public void UpgradeMaxHealth(int value)
        {
            _playerModel.UpdateMaxHealth(value);
            UIService.Instance.UpdateMaxHealthSlider(_playerModel.MaxHealth);
        }

        private bool CheckMaxExpLevelReached()
        {
            if (_playerModel.CurrentExpLevel>= expToUpgradeList.Count-1)
            {  
                return true;
            }
            return false;
        }
    }

}
