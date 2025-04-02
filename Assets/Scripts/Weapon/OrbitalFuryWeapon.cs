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
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class OrbitalFuryWeapon : WeaponController
    {
        [SerializeField] private GameObject _ballPrefab;
        private int _numBalls;
      
        private List<Ball> _balls = new List<Ball>();
        private Coroutine _orbitCoroutine;

        public override void Initialize(WeaponScriptableObject weapon_SO)
        {
            _numBalls = weapon_SO.initialProjectileCount;
            _minRadius = weapon_SO.minRadius;
            _maxRadius = weapon_SO.maxRadius;
            _speed = weapon_SO.speed;
            _attackPower=weapon_SO.attackPower;
            _cycleTime = weapon_SO.cycleTime;
            Weapon_SO = weapon_SO;
            CurrentWeaponLevel = 0;
            SubscribeToEvents();
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
            CreateBalls();
        }

        public override void DeactivateWeapon()
        {
            foreach (var ball in _balls)
            {
                Destroy(ball.gameObject);
            }
            _balls.Clear();
            gameObject.SetActive(false);
        }

        void CreateBalls()
        {
            if (_ballPrefab == null)
            {
                Debug.LogError("Ball prefab is not assigned!");
                return;
            }

            _balls.Clear();

            float angleStep = 360f / _numBalls;

            for (int i = 0; i < _numBalls; i++)
            {
                float angle = i * angleStep;
                Vector3 position = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * _maxRadius, Mathf.Sin(Mathf.Deg2Rad * angle) * _maxRadius, 0f);

                GameObject ballObject = Instantiate(_ballPrefab, position, Quaternion.identity, transform);
                Ball ball = ballObject.GetComponent<Ball>();

                if (ball != null)
                {
                    ball.Initialize(_attackPower, _speed, transform, _minRadius, _maxRadius, _cycleTime, _numBalls, i);
                    ball.StartOrbit();
                    _balls.Add(ball);
                }
                else
                {
                    Debug.LogError("Ball prefab does not have a Ball script attached!");
                }
            }
        }

        public override void ActivateUpgradeWeapon()
        {
            switch (Weapon_SO.powerUp_SO.powerUpList[CurrentWeaponLevel].powerUpTypes)
            {
                case PowerUpTypes.Activate:
                    ActivateWeapon();
                    break;
                case PowerUpTypes.NumOfProjectiles:
                    UpdateNumberOfBall();
                    break;
                case PowerUpTypes.Damage:
                    UpdateDamagePerBall();
                    break;
            }

            CurrentWeaponLevel++;
        }

        public void UpdateNumberOfBall()
        {
            DeactivateWeapon();
            _numBalls = (int)Weapon_SO.powerUp_SO.powerUpList[CurrentWeaponLevel].value;
            ActivateWeapon();
        }

        public void UpdateDamagePerBall()
        {
            DeactivateWeapon();
            _attackPower = (int)Weapon_SO.powerUp_SO.powerUpList[CurrentWeaponLevel].value; ;
            ActivateWeapon();
        }

    }

}

