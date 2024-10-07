using System;
using System.Collections.Generic;
using Framework.Stats;
using UnityEngine;

public interface IWeaponData
{
    public string Name { get; set; }
}

public interface IRangedWeapon
{
    
}

public interface IMeleeWeapon
{
    
}

[Serializable]
public class MightySword : IWeaponData, IMeleeWeapon
{
    public float power;
    [field: SerializeField] public string Name { get; set; }

    public GameObject model;
}

[Serializable]
public class MightShield : IWeaponData, IMeleeWeapon
{
    public float power;
    public float defense;
    [field: SerializeField] public string Name { get; set; }
}

[Serializable]
public class MightyGun : IWeaponData
{
    public string Name { get; set; }

    public GameObject bullet;
}

public class StatScaling
{
    public Stat stat;
    
}

[Serializable]
public class TestData : IWeaponData
{
    public string ahhhhhh;
    public string Name { get; set; }
}

[CreateAssetMenu(menuName = "Weapons/Weapon Asset", fileName = "Weapon Asset")]
public class WeaponAsset : ScriptableObject
{
    public GameObject weaponPrefab;
    [SerializeReference] public List<IWeaponData> weaponData = new List<IWeaponData>();

    [ContextMenu(nameof(MightShield))] public void MightShield() => weaponData.Add(new MightShield());
    [ContextMenu(nameof(MightySword))] public void MightySword() => weaponData.Add(new MightySword());
    [ContextMenu(nameof(MightyGun))] public void MightyGun() => weaponData.Add(new MightyGun());

    public void Tes()
    {
        foreach (var weapon in weaponData)
        {
            var arma = weapon as MightShield;
        }
    }
}