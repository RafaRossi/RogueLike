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
        
        protected WeaponHolder WeaponHolder { get; private set; }

        public virtual void UseWeapon()
        {
            WeaponStrategy.UseWeapon(this);
        }

        public T GetWeapon<T>() where T: Weapon
        {
            return this as T;
        }

        public WeaponHolder GetWeaponHolder()
        {
            return WeaponHolder;
        }

        public abstract class Builder<T> where T : Weapon
        {
            private GameObject _weaponPrefab;
            
            private string _weaponName;
            private WeaponStrategy _weaponStrategy;
            private WeaponHolder _weaponHolder;

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

            public Builder<T> WithWeaponHolder(WeaponHolder weaponHolder)
            {
                _weaponHolder = weaponHolder;
                return this;
            }

            public virtual T Build(Transform origin)
            {
                var weapon = _weaponPrefab == null ? new GameObject(_weaponName).AddComponent<Weapon>() : Instantiate(_weaponPrefab, origin.position, Quaternion.identity, origin).GetComponent<Weapon>();
                
                weapon.WeaponName = _weaponName;
                weapon.WeaponStrategy = _weaponStrategy;
                
                _weaponStrategy.InitializeWeapon(weapon);

                return weapon as T;
            }
        }
    }
    
    public interface IWeapon
    {
        void UseWeapon();

        T GetWeapon<T>() where T : Weapon;

        WeaponHolder GetWeaponHolder();
    }
}