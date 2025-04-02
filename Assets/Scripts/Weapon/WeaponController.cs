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

        public virtual void Initialize(WeaponScriptableObject weapon_SO) { }
        public virtual void ActivateWeapon() { }
        public virtual void DeactivateWeapon() { }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }
    }
}