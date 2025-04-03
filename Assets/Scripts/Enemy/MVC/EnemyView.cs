using Roguelike.DamageNumber;
using Roguelike.Main;
using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _enemy_RB;
        [SerializeField] private SpriteRenderer _enemy_SR;
        [SerializeField] private Animator _enemyAnimator;
        private Transform _playerTransform;
        private Vector3 _enemyDirection;
        private GameState _currentGameState;

        public EnemyController _controller { get; private set; }

        public void SetController(EnemyController controllerToSet) => _controller = controllerToSet;

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        void Update()
        {
            if (_currentGameState == GameState.Gameplay)
            {
               _controller?.UpdateEnemy();
            }
        }

        void FixedUpdate()
        {
            if(_currentGameState==GameState.Gameplay)
            {
                Move();
            }
            else
            {
                _enemy_RB.linearVelocity=Vector2.zero;
            }
        }

        public void Move()
        {
            _playerTransform = GameService.Instance.GetService<PlayerService>().GetPlayer().PlayerGameObject.transform;

            if (_playerTransform.position.x > transform.position.x)
            {
                _enemy_SR.flipX = true;
            }
            else
            {
                _enemy_SR.flipX = false;
            }

            _enemyDirection = (_playerTransform.position - transform.position).normalized;

            _enemy_RB.linearVelocity = new Vector2(_enemyDirection.x * _controller.GetEnemyModel.MovementSpeed,
                _enemyDirection.y * _controller.GetEnemyModel.MovementSpeed);
        }

        protected void OnCollisionStay2D(Collision2D collision)
        {
            PlayerView playerObj = collision.gameObject.GetComponent<PlayerView>();
            if (playerObj!=null)
            {
                playerObj.TakeDamage(_controller.GetEnemyModel.AttackPower);
                _controller.OnCollisionWithPlayer();
            }
        }

        public void TakeDamage(int damage)
        {
            GameService.Instance.GetService<DamageNumberService>().SpawnDamageNumber(transform.position, damage);
            _controller.TakeDamage(damage);
        }

    }
}