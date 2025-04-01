using Roguelike.Enemy;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Roguelike.Weapon
{
    public class PlayerProjectile : MonoBehaviour
    {
        public int damage;
        public float lifeTime;
        public float speed;
        [SerializeField] private Rigidbody2D rb;

        public void Initialize(Vector3 direction, int damage, float lifeTime, float speed)
        {
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
            DestroyProjectile();
        }

        private void RotateProjectile(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        private void DestroyProjectile()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyView enemyObj = other.gameObject.GetComponent<EnemyView>();
            if (enemyObj != null)
            {
                enemyObj.TakeDamage(damage);
                DestroyProjectile();
            }
        }
    }

}
