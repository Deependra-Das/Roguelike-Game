using Roguelike.Player;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Roguelike.Projectile
{
    public class EnemyProjectileView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        private int damage;
        private float lifeTime;
        private float speed;
        private bool isActive=false;
        private float timeAlive = 0f;

        public EnemyProjectileController _controller { get; private set; }

        public void SetController(EnemyProjectileController controllerToSet) => _controller = controllerToSet;

        public void Initialize(Vector3 spawnPosition, Vector3 direction, int damage, float lifeTime, float speed)
        {
            transform.position = spawnPosition;
            this.damage = damage;
            this.lifeTime = lifeTime;
            this.speed = speed;

            if (rb != null)
            {
                rb.linearVelocity = direction.normalized * speed;
            }
            timeAlive = 0;
            isActive = true;
        }

        void Update()
        {
            if(isActive)
            {
                timeAlive += Time.deltaTime;

                if (timeAlive >= lifeTime)
                {
                    ReturnProjectileToPool();
                }
            }
        }


        public void ReturnProjectileToPool()
        {
            isActive = false;
            gameObject.SetActive(false);
            _controller.ReturnProjectileToPool();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PlayerView playerObj = other.gameObject.GetComponent<PlayerView>();
            if (playerObj != null)
            {
                playerObj.TakeDamage(damage);
                ReturnProjectileToPool();
            }
        }
    }

}
