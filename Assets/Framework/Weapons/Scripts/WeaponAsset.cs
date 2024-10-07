using System;
using System.Collections.Generic;
using Framework.Stats;
using UnityEngine;

public interface IWeaponData
{
    
}

public interface IRangedWeapon
{
    
}

public interface IMeleeWeapon
{
    
}

public class MightySword : IWeaponData, IMeleeWeapon
{
    
}

public class StatScaling
{
    public Stat stat;
    
}

[Serializable]
public class TestData : IWeaponData
{
    public string ahhhhhh;
}

[CreateAssetMenu(menuName = "Weapons/Weapon Asset", fileName = "Weapon Asset")]
public class WeaponAsset : ScriptableObject
{
    public GameObject weaponPrefab;
    [SerializeReference] public IWeaponData weaponData;

    [ContextMenu(nameof(TestData))] public void Test() => weaponData = new TestData();
}