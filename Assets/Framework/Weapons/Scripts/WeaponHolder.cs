using System;
using Framework.Entities;
using Framework.Stats;
using Framework.Weapons.Weapon_Factories;
using UnityEngine;

namespace Framework.Weapons.Scripts
{
    public class WeaponHolder : MonoBehaviour
    {
        private IWeapon _currentWeapon;

        [SerializeField] private WeaponFactory initialWeaponFactory;

        [field:SerializeField] public EntityController EntityController { get; private set; }
        
        private void Start()
        {
            if (initialWeaponFactory != null)
            {
                InstantiateWeapon(initialWeaponFactory);
            }
        }

        public void UseWeaponRequest()
        {
            _currentWeapon?.UseWeapon();
        }

        private void InstantiateWeapon(WeaponFactory weaponFactory)
        {
            _currentWeapon = weaponFactory.CreateWeapon(transform);
        }

        public IWeapon GetCurrentWeapon() => _currentWeapon;
    }
}
