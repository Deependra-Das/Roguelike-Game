using Roguelike.Enemy;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Roguelike.Main;
using Roguelike.Sound;

namespace Roguelike.Weapon
{
    public class Ball : MonoBehaviour
    {
        private int _damage;
        private float _speed;
        private Transform _center;
        private float _minRadius;
        private float _maxRadius;
        private float _cycleTime;
        private float _radius;
        private int _totalBalls;
        private int _ballIndex;
        private Coroutine _orbitCoroutine;

        public void Initialize(int damage, float speed, Transform center, float minRadius, float maxRadius, float cycleTime, int totalBalls, int ballIndex)
        {
            _damage = damage;
            _speed = speed;
            _center = center;
            _minRadius = minRadius;
            _maxRadius = maxRadius;
            _cycleTime = cycleTime;
            _totalBalls = totalBalls;
            _ballIndex = ballIndex;
        }

        public void StartOrbit()
        {
            if (_orbitCoroutine != null)
            {
                StopCoroutine(_orbitCoroutine);
            }

            _orbitCoroutine = StartCoroutine(OrbitCoroutine());
        }

        private IEnumerator OrbitCoroutine()
        {
            float timeElapsed = 0f;

            while (true)
            {
                if (_center != null)
                {
                    timeElapsed += Time.deltaTime;
                    _radius = Mathf.Lerp(_minRadius, _maxRadius, Mathf.PingPong(timeElapsed / _cycleTime, 1f));
                    float angle = (360f / _totalBalls) * _ballIndex + (Time.time * _speed);
                    float angleInRadians = Mathf.Deg2Rad * angle;               
                    Vector3 offset = new Vector3(Mathf.Cos(angleInRadians) * _radius, Mathf.Sin(angleInRadians) * _radius, 0);        
                    transform.position = _center.position + offset;
                    GameService.Instance.GetService<SoundService>().PlayWeaponSFX(SoundType.OrbitalFury);
                }
                yield return null;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyView enemyObj = other.gameObject.GetComponent<EnemyView>();
            if (enemyObj != null)
            {
                enemyObj.TakeDamage(_damage);
            }
        }
    }

}
