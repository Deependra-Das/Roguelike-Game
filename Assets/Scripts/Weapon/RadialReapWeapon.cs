using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Player;
using Roguelike.Enemy;

namespace Roguelike.Weapon
{
    public class RadialReapWeapon : WeaponController
    {
        private Vector3 _originalScale;
        private Coroutine _shrinkGrowCoroutine;

        public override void Initialize(WeaponScriptableObject weapon_SO)
        {
            _attackPower = weapon_SO.attackPower;
            _minRadius = weapon_SO.minRadius;
            _maxRadius = weapon_SO.maxRadius;
            _cycleTime = weapon_SO.cycleTime;
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

        public override void ActivateWeapon()
        {
            gameObject.SetActive(true);
            _originalScale = transform.localScale;

            if (_shrinkGrowCoroutine == null)
            {
                _shrinkGrowCoroutine = StartCoroutine(ShrinkGrowCycle());
            }
        }

        public override void DeactivateWeapon()
        {
            if (_shrinkGrowCoroutine != null)
            {
                StopCoroutine(_shrinkGrowCoroutine);
                _shrinkGrowCoroutine = null;
            }

            gameObject.SetActive(false);
        }

        private IEnumerator ShrinkGrowCycle()
        {
            while (true)
            {
                yield return StartCoroutine(ScaleObject(_maxRadius, _minRadius));
                yield return StartCoroutine(ScaleObject(_minRadius, _maxRadius));
            }
        }

        private IEnumerator ScaleObject(float startRadius, float endRadius)
        {
            float elapsedTime = 0f;

            Vector3 startScale = _originalScale * startRadius;
            Vector3 endScale = _originalScale * endRadius;

            while (elapsedTime < _cycleTime)
            {
                transform.localScale = Vector3.Lerp(startScale, endScale, (elapsedTime / _cycleTime) * _speed);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localScale = endScale;
        }

        public override void ActivateUpgradeWeapon()
        {
            switch (Weapon_SO.powerUp_SO.powerUpList[CurrentWeaponLevel].powerUpTypes)
            {
                case PowerUpTypes.Activate:
                    ActivateWeapon();
                    break;
                case PowerUpTypes.Radius:
                    UpdateMaxRadius();
                    break;
                case PowerUpTypes.Damage:
                    UpdateAttackPower();
                    break;
            }

            CurrentWeaponLevel++;
        }

        public void UpdateMaxRadius()
        {
            DeactivateWeapon();
            _maxRadius = Weapon_SO.powerUp_SO.powerUpList[CurrentWeaponLevel].value;
            ActivateWeapon();
        }

        public void UpdateAttackPower()
        {
            DeactivateWeapon();
            _attackPower = (int)Weapon_SO.powerUp_SO.powerUpList[CurrentWeaponLevel].value;
            ActivateWeapon();
        }

        protected void OnTriggerStay2D(Collider2D collider)
        {
            EnemyView enemyObj = collider.gameObject.GetComponent<EnemyView>();
            if (enemyObj != null)
            {
                enemyObj.TakeDamage(_attackPower);
            }
        }
    }
}

