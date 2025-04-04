using Roguelike.Enemy;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Roguelike.Projectile
{
    public class PlayerProjectileView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _playerprojectile_RB;
        private int _damage;
        private float _lifeTime;
        private float _speed;
        private bool _isActive=false;
        private float _timeAlive = 0f;

        public PlayerProjectileController _controller { get; private set; }

        public void SetController(PlayerProjectileController controllerToSet) => _controller = controllerToSet;

        public void Initialize(Vector3 spawnPosition, Vector3 direction, int damage, float lifeTime, float speed)
        {
            transform.position = spawnPosition;
            this._damage = damage;
            this._lifeTime = lifeTime;
            this._speed = speed;

            if (_playerprojectile_RB != null)
            {
                _playerprojectile_RB.linearVelocity = direction.normalized * speed;
            }
            _timeAlive = 0;
            _isActive = true;
        }

        void Update()
        {
            if(_isActive)
            {
                _timeAlive += Time.deltaTime;

                if (_timeAlive >= _lifeTime)
                {
                    ReturnProjectileToPool();
                }
            }
        }

        public void ReturnProjectileToPool()
        {
            _isActive = false;
            gameObject.SetActive(false);
            _controller.ReturnProjectileToPool();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyView enemyObj = other.gameObject.GetComponent<EnemyView>();
            if (enemyObj != null)
            {
                enemyObj.TakeDamage(_damage);
                ReturnProjectileToPool();
            }
        }
    }

}
