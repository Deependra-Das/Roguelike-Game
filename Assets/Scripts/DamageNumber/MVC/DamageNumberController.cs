using Roguelike.Main;
using UnityEngine;

namespace Roguelike.DamageNumber
{
    public class DamageNumberController
    {
        private DamageNumberView _damageNumberView;

        public DamageNumberController(GameObject damageNumberPrefab)
        {
            GameObject newDamageNumberObject = Object.Instantiate(damageNumberPrefab);
            _damageNumberView = newDamageNumberObject.GetComponent<DamageNumberView>();
        }

        public void Configure(Vector2 spawnPosition, Transform damageCanvasTransform, int damageValue)
        {
            _damageNumberView.SetController(this);
            _damageNumberView.transform.SetParent(damageCanvasTransform,false);
            _damageNumberView.transform.position = spawnPosition;
            _damageNumberView.gameObject.SetActive(true);
            PlayTextAnimation(damageValue);
        }

        public void PlayTextAnimation(int damageValue)
        {
            _damageNumberView.PlayTextAnimation(damageValue);            
        }

        public void ReturnToPool()
        {
            _damageNumberView.gameObject.SetActive(false);
            ServiceLocator.Instance.GetService<DamageNumberService>().ReturnDamageNumberToPool(this);
        }

        public GameObject GetDamageNumberGameObject() { return _damageNumberView.gameObject; }
    }
}
