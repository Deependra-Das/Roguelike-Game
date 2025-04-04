using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Player;
using Roguelike.Enemy;
using Roguelike.Projectile;
using Roguelike.Sound;

namespace Roguelike.Weapon
{
    public class ScatterShotWeapon : WeaponController
    {
        [SerializeField] private GameObject projectilePrefab;
        private int _numberOfProjectiles;
        private Coroutine shootingCoroutine;    

        public override void Initialize(WeaponScriptableObject weapon_SO)
        {
            _numberOfProjectiles = weapon_SO.initialProjectileCount;
            _cycleTime = weapon_SO.cycleTime;
            _attackPower = weapon_SO.attackPower;
            _minRadius = weapon_SO.minRadius;
            _lifeTime = weapon_SO.lifeTime;
            _speed = weapon_SO.speed;
            Weapon_SO = weapon_SO;
            CurrentWeaponLevel = 0;
        
            SubscribeToEvents();
            gameObject.SetActive(false);
        }

        protected override void SubscribeToEvents()
        {
            EventService.Instance.OnGameOver.AddListener(OnGameOver);
        }

        protected override void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameOver.RemoveListener(OnGameOver);
        }

        private IEnumerator ShootingCoroutine()
        {
            while (true)
            {
                GameService.Instance.GetService<SoundService>().PlayWeaponSFX(SoundType.ScatterShot);
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

        public override void ActivateUpgradeWeapon()
        {
            switch(Weapon_SO.powerUp_SO.powerUpList[CurrentWeaponLevel].powerUpTypes)
            {
                case PowerUpTypes.Activate:
                    ActivateWeapon();
                    break;
                case PowerUpTypes.Radius:
                    UpdateLifeTimeForRadius();
                    break;
                case PowerUpTypes.NumOfProjectiles:
                    UpdateNumberOfProjectiles();
                    break;
            }           

            CurrentWeaponLevel++;
        }

        private void UpdateLifeTimeForRadius()
        {
            DeactivateWeapon();
            _lifeTime = Weapon_SO.powerUp_SO.powerUpList[CurrentWeaponLevel].value;
            ActivateWeapon();
        }

        private void UpdateNumberOfProjectiles()
        {
            DeactivateWeapon();
            _numberOfProjectiles = (int)Weapon_SO.powerUp_SO.powerUpList[CurrentWeaponLevel].value;
            ActivateWeapon();
        }

    }

}

