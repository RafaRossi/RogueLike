using Framework.Weapons.Scripts;
using UnityEngine;

namespace Framework.Weapons.Weapon_Strategies
{
    public abstract class WeaponStrategy : ScriptableObject
    {
        public abstract void InitializeWeapon(IWeapon weapon);
        public abstract void UseWeapon(IWeapon weapon);
    }
}
