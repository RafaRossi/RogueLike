using System;
using UnityEngine;

namespace Framework.Weapons.Scripts
{
    [CreateAssetMenu(menuName = "Weapons/Weapon Asset", fileName = "Weapon Asset")]
    public class WeaponAsset : ScriptableObject
    {
        public Weapon weaponPrefab;
    }
}