using Roguelike.Enemy;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Roguelike.Projectile
{
    public class PlayerProjectileView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        public int damage;
        public float lifeTime;
        public float speed;

        public PlayerProjectileController _controller { get; private set; }

        public void SetController(PlayerProjectileController controllerToSet) => _controller = controllerToSet;

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
            StartCoroutine(LifetimeCoroutine());
        }

        private IEnumerator LifetimeCoroutine()
        {
            yield return new WaitForSeconds(lifeTime);
            ReturnProjectileToPool();
        }

        //private void RotateProjectile(Vector3 direction)
        //{
        //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //}

        public void ReturnProjectileToPool()
        {
            gameObject.SetActive(false);
            _controller.ReturnProjectileToPool();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyView enemyObj = other.gameObject.GetComponent<EnemyView>();
            if (enemyObj != null)
            {
                enemyObj.TakeDamage(damage);
                ReturnProjectileToPool();
            }
        }
    }

}
