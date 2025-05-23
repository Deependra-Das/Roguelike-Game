using Roguelike.Main;
using Roguelike.Player;
using UnityEngine;
namespace Roguelike.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        protected float _cycleTime;
        protected int _attackPower;
        protected float _minRadius;
        protected float _maxRadius;
        protected float _lifeTime;
        protected float _speed;
        public int CurrentWeaponLevel { get; protected set; }
        public WeaponScriptableObject Weapon_SO { get; protected set; }

        ~WeaponController() => UnsubscribeToEvents();
        public virtual void Initialize(WeaponScriptableObject weapon_SO) { }
        public virtual void ActivateWeapon() { }
        public virtual void DeactivateWeapon() { }
        protected virtual void SubscribeToEvents() { }
        protected virtual void UnsubscribeToEvents() { }
        public virtual void ActivateUpgradeWeapon() { }

        protected void OnGameOver()
        {
            if (ServiceLocator.Instance.GetService<PlayerService>().GetPlayer() != null)
            {
                DeactivateWeapon();
                Destroy(this.gameObject);
            }  
        }

    }
}