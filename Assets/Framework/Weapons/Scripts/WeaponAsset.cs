using System;
using System.Collections.Generic;
using Framework.Behaviours.Attack;
using UnityEngine;

namespace Framework.Weapons.Scripts
{
    [CreateAssetMenu(menuName = "Weapons/Weapon Asset", fileName = "Weapon Asset")]
    public class WeaponAsset : ScriptableObject
    {
        public Weapon weaponPrefab;
        public List<AttackData> attackData;
    }
}