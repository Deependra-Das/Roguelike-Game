using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.DamageNumber
{
    public class DamageNumberPool : GenericObjectPool<DamageNumberController>
    {
        private GameObject _damageNumberPrefab;

        public DamageNumberPool(GameObject damageNumberPrefab)
        {
            _damageNumberPrefab = damageNumberPrefab;
        }

        public DamageNumberController GetDamageNumber<T>(GameObject damageNumberPrefab) where T : DamageNumberController
        {
            _damageNumberPrefab = damageNumberPrefab;
            return GetItem<T>();
        }

        protected override DamageNumberController CreateItem<T>()
        {
            return new DamageNumberController(_damageNumberPrefab);
        }

        public void ResetPool()
        {
            foreach (var pooledItem in pooledItems)
            {
                UnityEngine.Object.Destroy(pooledItem.Item.GetDamageNumberGameObject());
            }

            pooledItems.Clear();
        }
    }
}
