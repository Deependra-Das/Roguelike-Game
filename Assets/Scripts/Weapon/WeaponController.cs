using Roguelike.Main;
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
        protected GameState _currentGameState;
        public WeaponScriptableObject Weapon_SO { get; protected set; }

        public virtual void Initialize(WeaponScriptableObject weapon_SO) { }
        public virtual void ActivateWeapon() { }
        public virtual void DeactivateWeapon() { }
        protected virtual void SubscribeToEvents() { }
        protected virtual void UnsubscribeToEvents() { }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        protected void OnGameOver()
        {
            DeactivateWeapon();
            Destroy(this.gameObject);
        }

        protected void OnDestroy() => UnsubscribeToEvents();
    }
}