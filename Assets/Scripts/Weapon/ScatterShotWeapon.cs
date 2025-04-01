using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Player;
using Roguelike.Enemy;

namespace Roguelike.Weapon
{
    public class ScatterShotWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject projectilePrefab;
        private int numberOfProjectiles;
        private float interval;
        private int damage;
        private float radius;
        private float lifeTime;
        private float speed;
        private Coroutine shootingCoroutine;
        private GameState _currentGameState;

        public void Initialize(WeaponScriptableObject weapon_SO)
        {
            numberOfProjectiles = 8;
            interval = weapon_SO.cycleTime;
            damage = weapon_SO.attackPower;
            radius = weapon_SO.maxRadius;
            lifeTime = weapon_SO.lifeTime;
            speed = weapon_SO.speed;
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
        }

        private IEnumerator ShootingCoroutine()
        {
            while (true)
            {
                ShootProjectiles();
                yield return new WaitForSeconds(interval);
            }
        }

        private void ShootProjectiles()
        {
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float angle = (i * 360f) / numberOfProjectiles;
                Vector3 direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * radius, Mathf.Sin(Mathf.Deg2Rad * angle) * radius, 0f);
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
                projectileScript.Initialize(direction, damage, lifeTime, speed);
            }
        }

        public void ActivateWeapon()
        {
            gameObject.SetActive(true);
            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(ShootingCoroutine());
            }
        
        }

        public void DeactivateWeapon()
        {
            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
            gameObject.SetActive(false);
        }

        private void OnGameOver()
        {
            DeactivateWeapon();
            Destroy(this.gameObject);
        }

    }

}

