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

    public class OrbitalFuryWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject _ballPrefab;
        private int _numBalls;
        private float _minRadius;
        private float _maxRadius;
        private float _speed;
        private int _damagePerBall;
        private float _cycleTime;
        private GameState _currentGameState;

        private List<Ball> _balls = new List<Ball>();
        private Coroutine _orbitCoroutine;

        public void Initialize(WeaponScriptableObject weapon_SO)
        {
            _numBalls = 1;
            _minRadius = weapon_SO.minRadius;
            _maxRadius = weapon_SO.maxRadius;
            _speed = weapon_SO.speed;
            _damagePerBall=weapon_SO.attackPower;
            _cycleTime = weapon_SO.cycleTime;
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

        public void ActivateWeapon()
        {
            gameObject.SetActive(true);
            CreateBalls();
        }

        public void DeactivateWeapon()
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
                    ball.Initialize(_damagePerBall, _speed, transform, _minRadius, _maxRadius, _cycleTime, _numBalls, i);
                    ball.StartOrbit();
                    _balls.Add(ball);
                }
                else
                {
                    Debug.LogError("Ball prefab does not have a Ball script attached!");
                }
            }
        }

        public void UpdateNumberOfBall(int newNumBalls)
        {
            DeactivateWeapon();
            _numBalls = newNumBalls;
            ActivateWeapon();
        }

        public void UpdateDamagePerBall(int newDamagePerBall)
        {
            DeactivateWeapon();
            _damagePerBall = newDamagePerBall;
            ActivateWeapon();
        }

        private void OnGameOver()
        {
            DeactivateWeapon();
            Destroy(this.gameObject);
        }

    }

}

