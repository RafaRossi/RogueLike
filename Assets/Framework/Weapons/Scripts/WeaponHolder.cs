using System;
using Framework.Entities;
using Framework.Weapons.Scripts;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Weapons.Scripts
{
    public class WeaponHolder : MonoBehaviour
    {
        private IWeapon _currentWeapon;

        [SerializeField] private WeaponAsset initialWeaponAsset;

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
            
            _currentWeapon = CreateWeapon(weaponAsset.weaponPrefab, transform);
            
            onEquipWeapon?.Invoke(_currentWeapon);
        }

        public IWeapon GetCurrentWeapon() => _currentWeapon;

        private static IWeapon CreateWeapon(GameObject weaponPrefab, Transform origin)
        {
            return new Builder().Build(weaponPrefab, origin);
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
        
            public IWeapon Build(GameObject weaponPrefab, Transform origin)
            {
                var weapon = Instantiate(weaponPrefab, _position, _rotation, origin);

                return weapon.GetComponent<IWeapon>();
            }
        }
    }
}

public interface IWeapon
{
    void UseWeapon();
}