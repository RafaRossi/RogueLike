using Framework.Weapons.Scripts;
using Framework.Weapons.Weapon_Strategies;
using UnityEngine;

namespace Framework.Weapons.Weapon_Factories
{
    public abstract class WeaponFactory : ScriptableObject
    {
        [SerializeField] protected GameObject weaponPrefab;
        [SerializeField] protected WeaponStrategy weaponStrategy; 
        
        public abstract IWeapon CreateWeapon(Transform origin);
    }
}
