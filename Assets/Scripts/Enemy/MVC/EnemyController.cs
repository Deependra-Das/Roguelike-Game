using Roguelike.Main;
using UnityEngine;

namespace Roguelike.Enemy
{
    public class EnemyController
    {
        private EnemyModel _enemyModel;
        private EnemyView _enemyView;

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
            _enemyView.gameObject.SetActive(true);
        }

        public EnemyModel GetEnemyModel { get { return _enemyModel; } }
    }
}
