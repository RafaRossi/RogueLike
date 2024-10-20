using System;
using Framework.Entities;
using Framework.Weapons.Scripts;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Framework.Weapons.Scripts
{
    public class WeaponHolder : MonoBehaviour
    {
        private IWeapon _currentWeapon;

        [SerializeField] private WeaponAsset currentWeaponAsset;

        public UnityEvent<IWeapon> onEquipWeapon = new UnityEvent<IWeapon>();
        public UnityEvent<IWeapon> onUnEquipWeapon = new UnityEvent<IWeapon>();
        
        private void Start()
        {
            if (currentWeaponAsset != null)
            {
                InstantiateWeapon(currentWeaponAsset);
            }
        }

        public void UseWeaponPrimaryRequest()
        {
            _currentWeapon.UseWeaponPrimary();
        }

        public void UseWeaponSecondaryRequest()
        {
            _currentWeapon.UseWeaponSecondary();
        }

        private void InstantiateWeapon(WeaponAsset weaponAsset)
        {
            if(_currentWeapon != null) onUnEquipWeapon?.Invoke(_currentWeapon);
            
            _currentWeapon = CreateWeapon(weaponAsset, this);
            
            onEquipWeapon?.Invoke(_currentWeapon);
        }

        public IWeapon GetCurrentWeapon() => _currentWeapon;

        public WeaponAsset GetWeaponAsset() => currentWeaponAsset;

        private static Weapon CreateWeapon(WeaponAsset weaponAsset, WeaponHolder origin)
        {
            return new Builder().WithPosition(origin.transform.position).WithRotation(Quaternion.identity).Build(weaponAsset, origin);
        }

        private class Builder
        {
            private Vector3 _position = Vector3.zero;
            private Quaternion _rotation = Quaternion.identity;

            public Builder WithPosition(Vector3 position)
            {
                _position = position;

                return this;
            }

            public Builder WithRotation(Quaternion rotation)
            {
                _rotation = rotation;
            
                return this;
            }
        
            public Weapon Build(WeaponAsset weaponAsset, WeaponHolder origin)
            {
                return Instantiate(weaponAsset.weaponPrefab, _position, _rotation, origin.transform).Initialize(origin);
            }
        }
    }
}

public interface IWeapon
{
    public string Name { get; set; }

    void UseWeaponPrimary();
    void UseWeaponSecondary();
}