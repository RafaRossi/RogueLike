using Framework.Weapons.Weapon_Strategies;
using Project.Utils;
using Unity.Mathematics;
using UnityEngine;

namespace Framework.Weapons.Scripts
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public string WeaponName { get; private set; }
        protected WeaponStrategy WeaponStrategy { get; private set; }

        public virtual void UseWeapon(WeaponHolder weaponHolder)
        {
            WeaponStrategy.UseWeapon(weaponHolder.GetCurrentWeapon());
        }

        public T GetWeapon<T>() where T: Weapon
        {
            return this as T;
        }

        public abstract class Builder<T> where T : Weapon
        {
            private GameObject _weaponPrefab;
            
            private string _weaponName;
            private WeaponStrategy _weaponStrategy;

            public Builder<T> WithName(string weaponName)
            {
                _weaponName = weaponName;
                return this;
            }

            public Builder<T> WithWeaponPrefab(GameObject weaponPrefab)
            {
                _weaponPrefab = weaponPrefab;
                return this;
            }

            public Builder<T> WithWeaponStrategy(WeaponStrategy weaponStrategy)
            {
                _weaponStrategy = weaponStrategy;
                return this;
            }

            public virtual T Build(Transform origin)
            {
                var weapon = _weaponPrefab == null ? new GameObject(_weaponName).AddComponent<Weapon>() : Instantiate(_weaponPrefab, origin.position, Quaternion.identity, origin).GetComponent<Weapon>();
                
                weapon.WeaponName = _weaponName;
                weapon.WeaponStrategy = _weaponStrategy;

                return weapon as T;
            }
        }
    }
    
    public interface IWeapon
    {
        void UseWeapon(WeaponHolder weaponHolder);

        T GetWeapon<T>() where T : Weapon;
    }
}