using System;
using Framework.Entities;
using Framework.Stats;
using Framework.Weapons.Weapon_Factories;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Weapons.Scripts
{
    public class WeaponHolder : MonoBehaviour
    {
        private IWeapon _currentWeapon;

        [SerializeField] private WeaponAsset initialWeaponAsset;

        [field:SerializeField] public EntityController EntityController { get; private set; }

        public UnityEvent<IWeapon> onEquipWeapon = new UnityEvent<IWeapon>();
        public UnityEvent<IWeapon> onUnEquipWeapon = new UnityEvent<IWeapon>();
        
        private void Start()
        {
            if (initialWeaponAsset != null)
            {
                InstantiateWeapon(initialWeaponAsset);
            }
        }

        public void UseWeaponRequest()
        {
            _currentWeapon?.UseWeapon();
        }

        private void InstantiateWeapon(WeaponAsset weaponAsset)
        {
            if(_currentWeapon != null) onUnEquipWeapon?.Invoke(_currentWeapon);
            
            _currentWeapon = weaponAsset.CreateWeapon(transform);
            
            onEquipWeapon?.Invoke(_currentWeapon);
        }

        public IWeapon GetCurrentWeapon() => _currentWeapon;
    }
}
