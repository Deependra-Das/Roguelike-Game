using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Player;
using Roguelike.Enemy;

namespace Roguelike.Weapon
{
    public class RadialReapWeapon : MonoBehaviour, IWeapon
    {
        private int _attackPower;
        private float _minRadius;
        private float _maxRadius;
        private float _cycleTime;
        private float _speed;

        private Vector3 _originalScale;
        private Coroutine _shrinkGrowCoroutine;
        private GameState _currentGameState;
        public WeaponScriptableObject Weapon_SO { get; private set; }
        public void Initialize(WeaponScriptableObject weapon_SO)
        {
             Weapon_SO = weapon_SO;
            _attackPower =weapon_SO.attackPower;
            _minRadius = weapon_SO.minRadius;
            _maxRadius = weapon_SO.maxRadius;
            _cycleTime = weapon_SO.cycleTime;
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

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public void ActivateWeapon()
        {
            gameObject.SetActive(true);
            _originalScale = transform.localScale;

            if (_shrinkGrowCoroutine == null)
            {
                _shrinkGrowCoroutine = StartCoroutine(ShrinkGrowCycle());
            }
        }

        public void DeactivateWeapon()
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

        public void UpdateMaxRadius(float newMaxRadius)
        {
            DeactivateWeapon();
            _maxRadius = newMaxRadius;
            ActivateWeapon();
        }

        public void UpdateAttackPower(int newAttackPower)
        {
            _attackPower = newAttackPower;
        }

        private void OnGameOver()
        {
            DeactivateWeapon();
            Destroy(this.gameObject);
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

