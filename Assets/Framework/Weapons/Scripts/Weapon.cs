using UnityEngine;

namespace Framework.Weapons.Scripts
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public string Name { get; set; }

        public abstract void UseWeaponPrimary();

        public abstract void UseWeaponSecondary();

        public abstract Weapon Initialize(WeaponHolder weaponHolder);
    }
}
