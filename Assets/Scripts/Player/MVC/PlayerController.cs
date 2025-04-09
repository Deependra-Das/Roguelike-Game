using Roguelike.Main;
using Roguelike.Event;
using Roguelike.Enemy;
using Roguelike.Level;
using UnityEngine;
using System.Collections.Generic;
using Roguelike.UI;
using Roguelike.Weapon;
using Roguelike.Sound;
using Roguelike.Utilities;

namespace Roguelike.Player
{
    public class PlayerController
    {
        private UIService uiService;
        private PlayerModel _playerModel;
        private PlayerView _playerView;
        private GameState _currentGameState;
        private List<WeaponController> _weapons = new List<WeaponController>();
        protected bool isDead;
        protected List<int> expToUpgradeList;

        public PlayerController(PlayerData playerData)
        {
            uiService = ServiceLocator.Instance.GetService<UIService>();
            InitializeModel(playerData);
            InitializeView();
            SubscribeToEvents();
            UpdateInitialHealthOnUI();
            AddWeapon();
        }

        ~PlayerController() => UnsubscribeToEvents();

        private void InitializeModel(PlayerData playerData)
        {
            _playerModel = new PlayerModel(playerData);
        }

        private void InitializeView()
        {
            _playerView = Object.Instantiate(_playerModel.PlayerPrefab);
            _playerView.transform.position = _playerModel.SpawnPosition;
            _playerView.transform.rotation = Quaternion.Euler(_playerModel.SpawnRotation);
            _playerView.SetController(this);
        }

        private void UpdateInitialHealthOnUI()
        {
            expToUpgradeList = ServiceLocator.Instance.GetService<LevelService>().GetExpToUpgradeList();
            uiService.UpdateMaxHealthSlider(_playerModel.MaxHealth);
            uiService.UpdateCurrentHealthSlider(_playerModel.CurrentHealth);
            uiService.UpdateMaxExpSlider(expToUpgradeList[_playerModel.CurrentExpLevel]);
            uiService.UpdateCurrentExpSlider(_playerModel.CurrentExpPoints);
        }

        private void AddWeapon()
        {
            WeaponService weaponService = ServiceLocator.Instance.GetService<WeaponService>();
            _weapons.Clear();
            _weapons.Add(weaponService.CreateWeapons(WeaponType.RadialReap,_playerView.playerWeaponTransform));
            _weapons.Add(weaponService.CreateWeapons(WeaponType.OrbitalFury, _playerView.playerWeaponTransform));
            _weapons.Add(weaponService.CreateWeapons(WeaponType.ScatterShot, _playerView.playerWeaponTransform));
            EventService.Instance.OnWeaponAdded.Invoke(_weapons);
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
                uiService.UpdateCurrentHealthSlider(_playerModel.CurrentHealth);
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
            uiService.UpdateCurrentExpSlider(_playerModel.CurrentExpPoints);

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

            uiService.UpdateCurrentExpSlider(_playerModel.CurrentExpPoints);
            _playerModel.UpdateExpLevel();
            uiService.UpdateMaxExpSlider(expToUpgradeList[_playerModel.CurrentExpLevel]);

            ServiceLocator.Instance.GetService<SoundService>().PlaySFX(SoundType.LevelUp);
            GameService.Instance.ChangeGameState(GameState.PowerUpSelection);
        }

        public void Heal()
        {
            int amountToHeal = _playerModel.MaxHealth - _playerModel.CurrentHealth;
            _playerModel.UpdateCurrentHealth(amountToHeal);
            uiService.UpdateCurrentHealthSlider(_playerModel.CurrentHealth);
        }

        public void UpgradeMaxHealth(int value)
        {
            _playerModel.UpdateMaxHealth(value);
            uiService.UpdateMaxHealthSlider(_playerModel.MaxHealth);
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
