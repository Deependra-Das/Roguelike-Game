using Roguelike.Main;
using UnityEngine;

namespace Roguelike.Enemy
{
    public class EnemyController
    {
        protected EnemyModel _enemyModel;
        protected EnemyView _enemyView;
        protected float lastAttackTime;
        protected bool isDead;

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

        public void Configure()
        {
            isDead = false;
            lastAttackTime = 0;
            _enemyView.gameObject.SetActive(true);
        }
        public void UpdateEnemy()
        {
            if (isDead) return;
            Attack();
        }

        protected virtual void Attack() { }

        public EnemyModel GetEnemyModel { get { return _enemyModel; } }

        public void TakeDamage(int damage)
        {
            _enemyModel.UpdateHealth(-damage);
            if (_enemyModel.Health <= 0)
            {
                isDead = true;
                OnEnemyDeath();
            }
        }

        public void OnEnemyDeath()
        {
            _enemyView.gameObject.SetActive(false);
            GameService.Instance.GetService<EnemyService>().ReturnEnemyToPool(this);
        }

        public virtual void OnCollisionWithPlayer() { }
    }
}
