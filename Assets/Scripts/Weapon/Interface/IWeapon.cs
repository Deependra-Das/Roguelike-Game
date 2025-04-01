namespace Roguelike.Weapon
{
    public interface IWeapon
    {
        void Initialize(WeaponScriptableObject weapon_SO);
        void ActivateWeapon();
        void DeactivateWeapon();
    }
}