using Framework.Weapons.Scripts;
using Framework.Weapons.Weapon_Strategies;
using UnityEngine;

namespace Framework.Weapons.Weapon_Factories
{
    public abstract class WeaponFactory : ScriptableObject
    {
        public abstract IWeapon CreateWeapon(Transform origin, WeaponAsset weaponAsset);
    }
}
