using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.VFX
{
    public class VFXPool : GenericObjectPool<VFXController>
    {
        private GameObject _vfxPrefab;

        public VFXPool(GameObject vfxPrefab)
        {
            _vfxPrefab = vfxPrefab;
        }

        public VFXController GetVFX<T>(GameObject vfxPrefab) where T : VFXController
        {
            _vfxPrefab = vfxPrefab;
            return GetItem<T>();
        }

        protected override VFXController CreateItem<T>()
        {
               return new VFXController(_vfxPrefab);
        }

        public void ResetPool()
        {
            foreach (var pooledItem in pooledItems)
            {
                UnityEngine.Object.Destroy(pooledItem.Item.GetVFXGameObject());
            }

            pooledItems.Clear();
        }
    }
}
