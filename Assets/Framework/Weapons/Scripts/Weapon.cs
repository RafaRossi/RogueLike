using Framework.Weapons.Weapon_Strategies;
using Unity.Mathematics;
using UnityEngine;

namespace Framework.Weapons.Scripts
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public string WeaponName { get; private set; }
        public WeaponStrategy WeaponStrategy { get; private set; }

        public virtual void UseWeapon()
        {
            WeaponStrategy.UseWeapon(transform, this);
        }

        public abstract class Builder
        {
            private GameObject _weaponPrefab;
            
            private string _weaponName;
            private WeaponStrategy _weaponStrategy;

            public Builder WithName(string weaponName)
            {
                _weaponName = weaponName;
                return this;
            }

            public Builder WithWeaponPrefab(GameObject weaponPrefab)
            {
                _weaponPrefab = weaponPrefab;
                return this;
            }

            public Builder WithWeaponStrategy(WeaponStrategy weaponStrategy)
            {
                _weaponStrategy = weaponStrategy;
                return this;
            }

            public Weapon Build(Transform origin)
            {
                var weapon = _weaponPrefab == null ? new GameObject(_weaponName).AddComponent<Weapon>() : Instantiate(_weaponPrefab, origin.position, Quaternion.identity, origin).GetComponent<Weapon>();
                
                weapon.WeaponName = _weaponName;
                weapon.WeaponStrategy = _weaponStrategy;

                return weapon;
            }
        }
    }
    
    public interface IWeapon
    {
        void UseWeapon();
    }
}