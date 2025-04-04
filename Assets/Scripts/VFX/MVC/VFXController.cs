using Roguelike.Enemy;
using Roguelike.Main;
using UnityEngine;

namespace Roguelike.VFX
{
    public class VFXController
    {
        private VFXView _vfxView;
        
        public VFXController(GameObject vfxPrefab)
        {
            GameObject newVFXObject = Object.Instantiate(vfxPrefab);
            _vfxView = newVFXObject.GetComponent<VFXView>();
        }

        public void Configure(Vector2 spawnPosition)
        {
            _vfxView.transform.position = spawnPosition;
            _vfxView.gameObject.SetActive(true);
            PlayAnimation();
        }
       
        public void PlayAnimation()
        {
            _vfxView.PlayAnimation();
            GameService.Instance.GetService<VFXService>().ReturnVFXToPool(this);
        }

        public GameObject GetVFXGameObject() { return _vfxView.gameObject; }
    }
}