using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Player;
using Roguelike.Enemy;
using Roguelike.Projectile;

namespace Roguelike.Weapon
{
    public class ScatterShotWeapon : WeaponController
    {
        [SerializeField] private GameObject projectilePrefab;

        private int _numberOfProjectiles;
        private Coroutine shootingCoroutine;    

        public override void Initialize(WeaponScriptableObject weapon_SO)
        {
            _numberOfProjectiles = 8;
            _cycleTime = weapon_SO.cycleTime;
            _attackPower = weapon_SO.attackPower;
            _minRadius = weapon_SO.minRadius;
            _lifeTime = weapon_SO.lifeTime;
            _speed = weapon_SO.speed;
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

        private IEnumerator ShootingCoroutine()
        {
            while (true)
            {
                ShootProjectiles();
                yield return new WaitForSeconds(_cycleTime);
            }
        }

        private void ShootProjectiles()
        {
            for (int i = 0; i < _numberOfProjectiles; i++)
            {
                float angle = (i * 360f) / _numberOfProjectiles;
                Vector3 direction = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * _minRadius, Mathf.Sin(Mathf.Deg2Rad * angle) * _minRadius, 0f);
                GameService.Instance.GetService<ProjectileService>().SpawnProjectile(ProjectileType.PlayerBall, transform.position, direction, _attackPower, _lifeTime, _speed);
            }
        }

        public override void ActivateWeapon()
        {
            gameObject.SetActive(true);
            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(ShootingCoroutine());
            }
        
        }

        public override void DeactivateWeapon()
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

