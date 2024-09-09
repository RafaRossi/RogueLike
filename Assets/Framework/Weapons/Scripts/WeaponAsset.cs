using System.Collections.Generic;
using Framework.Stats;
using Framework.Weapons.Weapon_Factories;
using Framework.Weapons.Weapon_Strategies;
using UnityEngine;

namespace Framework.Weapons.Scripts
{
    [CreateAssetMenu(fileName = "Weapon Asset", menuName = "Weapon Data")]
    public class WeaponAsset : ScriptableObject
    {
        [field: SerializeField] public GameObject WeaponPrefab { get; private set; }
        [field: SerializeField] public WeaponStrategy WeaponStrategy { get; private set; }
        [field: SerializeField] public WeaponFactory WeaponFactory { get; private set; }
        
        [field: SerializeField] public List<StatModifier> StatModifiers { get; private set; }

        public IWeapon CreateWeapon(Transform origin)
        {
            return WeaponFactory.CreateWeapon(origin, this);
        }
    }
}
