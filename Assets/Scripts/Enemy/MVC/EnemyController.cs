using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Player;
using Roguelike.VFX;
using UnityEngine;

namespace Roguelike.Enemy
{
    public class EnemyController
    {
        protected EnemyModel _enemyModel;
        protected EnemyView _enemyView;
        protected float lastAttackTime;
        protected bool isDead;
        private GameState _currentGameState;

        public EnemyController(EnemyScriptableObject enemySO)
        {
            InitializeController(enemySO);
        }

        private void InitializeController(EnemyScriptableObject enemySO)
        {
            InitializeModel(enemySO);
            InitializeView();
        }

        private void InitializeModel(EnemyScriptableObject enemySO)
        {
            _enemyModel = new EnemyModel(enemySO);
        }

        private void InitializeView()
        {
            _enemyView = Object.Instantiate(_enemyModel.EnemyPrefab);
            _enemyView.transform.position = _enemyModel.SpawnPosition;
            _enemyView.transform.rotation = Quaternion.Euler(_enemyModel.SpawnRotation);
            _enemyView.SetController(this);
        }

        public void Configure(Vector2 spawnPosition)
        {
            isDead = false;
            lastAttackTime = 0;
            _enemyView.transform.position = spawnPosition;
            SubscribeToEvents();
            SetGameState(GameService.Instance.GameState);
            _enemyView.gameObject.SetActive(true);
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

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
            _enemyView.SetGameState(_currentGameState);
        }

        public void UpdateEnemy()
        {
            if (isDead || _currentGameState != GameState.Gameplay) return;
            Attack();
        }

        protected virtual void Attack() { }

        public EnemyModel GetEnemyModel { get { return _enemyModel; } }

        public void TakeDamage(int damage)
        {
            if(!isDead && _currentGameState == GameState.Gameplay)
            {
                _enemyModel.UpdateHealth(-damage);
                if (_enemyModel.Health <= 0)
                {
                    isDead = true;
                    OnEnemyDeath();
                }
            }           
        }

        private void OnGameOver()
        {
            _enemyModel.UpdateHealth(_enemyModel.Health);
            isDead = true;
            OnEnemyDeath();
        }

        public void OnEnemyDeath()
        {   
            if(_currentGameState==GameState.Gameplay)
            {
                GameService.Instance.GetService<PlayerService>().GetPlayer().AddExperiencePoints(_enemyModel.ExpDrop);
            }            
            UnsubscribeToEvents();
            GameService.Instance.GetService<VFXService>().SpawnVFX(new Vector2(_enemyView.gameObject.transform.position.x, _enemyView.gameObject.transform.position.y));
            _enemyView.gameObject.SetActive(false);
            GameService.Instance.GetService<EnemyService>().ReturnEnemyToPool(this);
        }

        public virtual void OnCollisionWithPlayer() { }

        public GameObject GetEnemyGameObject() { return _enemyView.gameObject; }
    }
}
