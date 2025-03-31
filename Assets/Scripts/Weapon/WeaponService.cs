using Roguelike.Event;
using Roguelike.Main;
using Roguelike.Utilities;
using UnityEngine;
using System.Collections.Generic;

namespace Roguelike.Weapon
{
    public class WeaponService : IService
    {
        private GameState _currentGameState;
        private List<WeaponScriptableObject> _weapon_SO_List;

        public WeaponService(List<WeaponScriptableObject> weapon_SO_List)
        {
            _weapon_SO_List = weapon_SO_List;
        }

        ~WeaponService() => UnsubscribeToEvents();

        public void Initialize(params object[] dependencies)
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.AddListener(SetGameState);
        }

        private void UnsubscribeToEvents()
        {
            EventService.Instance.OnGameStateChange.RemoveListener(SetGameState);
        }

        public void SetGameState(GameState _newState)
        {
            _currentGameState = _newState;
        }

        public IWeapon CreateWeapons(WeaponType type, Transform spawnTransform)
        {
            WeaponScriptableObject weaponData = _weapon_SO_List.Find(weapon => weapon.weaponType == type);
            IWeapon weapon = null;
            GameObject weaponObject = null;

            if (weaponData == null)
            {
                Debug.LogError("Weapon data not found for type: " + type);
                return null;
            }

            switch (type)
            {
                case WeaponType.RadialReap:
                    weaponObject = Object.Instantiate(weaponData.weaponPrefab, spawnTransform.position, Quaternion.identity, spawnTransform);
                    break;

                default:
                    Debug.LogWarning("Weapon type not handled: " + type);
                    break;
       
            }
            if (weaponObject != null)
            {
                weapon = weaponObject.GetComponent<IWeapon>();
                weapon.Initialize(weaponData);
            }
            else
            {
                Debug.LogError("Weapon prefab not Instantiated.");
            }

            return weapon;
        }

    }
}
