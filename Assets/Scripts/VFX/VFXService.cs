using Roguelike.Enemy;
using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.VFX
{
    public class VFXService : IService
    {
        private GameObject _vfxPrefab;
        private VFXPool _vfxPoolObj;

        public VFXService(GameObject vfxPrefab)
        {
            _vfxPrefab=vfxPrefab;
        }

        ~VFXService() => UnsubscribeToEvents();

        public void Initialize(params object[] dependencies)
        {
            _vfxPoolObj = new VFXPool(_vfxPrefab);
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnMainMenu.AddListener(ResetPool);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnMainMenu.RemoveListener(ResetPool);
        }

        public void SpawnVFX(Vector3 position)
        {
            VFXController vfx= _vfxPoolObj.GetVFX<VFXController>(_vfxPrefab);
            vfx.Configure(position);
        }

        public void ReturnVFXToPool(VFXController vfxToReturn) => _vfxPoolObj.ReturnItem(vfxToReturn);

        private void ResetPool()
        {
            _vfxPoolObj.ResetPool();
        }
    }
}