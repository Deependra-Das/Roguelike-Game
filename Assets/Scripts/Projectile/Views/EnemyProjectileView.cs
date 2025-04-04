using Roguelike.Player;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Roguelike.Projectile
{
    public class EnemyProjectileView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _enemyProjectile_RB;
        private int _damage;
        private float _lifeTime;
        private float _speed;
        private bool _isActive=false;
        private float _timeAlive = 0f;

        public EnemyProjectileController _controller { get; private set; }

        public void SetController(EnemyProjectileController controllerToSet) => _controller = controllerToSet;

        public void Initialize(Vector3 spawnPosition, Vector3 direction, int damage, float lifeTime, float speed)
        {
            transform.position = spawnPosition;
            this._damage = damage;
            this._lifeTime = lifeTime;
            this._speed = speed;

            if (_enemyProjectile_RB != null)
            {
                _enemyProjectile_RB.linearVelocity = direction.normalized * speed;
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
            PlayerView playerObj = other.gameObject.GetComponent<PlayerView>();
            if (playerObj != null)
            {
                playerObj.TakeDamage(_damage);
                ReturnProjectileToPool();
            }
        }
    }

}
