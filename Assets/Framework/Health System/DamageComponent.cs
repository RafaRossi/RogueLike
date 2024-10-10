using System;
using System.Collections;
using System.Collections.Generic;
using Framework.Behaviours.Animations;
using Framework.Health_System;
using Framework.State_Machine;
using UnityEngine;
using UnityEngine.Events;

public class DamageComponent : BaseComponent<DamageComponent>, IDamageable
{
    [SerializeField] private HealthComponent healthComponent;
    
    public void TakeDamage(Damage damage)
    {
        var currentHealth = healthComponent.CurrentHealth;

        //currentHealth -= damage.damageAmount;
        healthComponent.SetCurrentHealth(currentHealth);

        OnTakeDamage?.Invoke(damage);
    }

    [field:SerializeField] public UnityEvent<Damage> OnTakeDamage { get; set; }
}

public interface IDamageable
{
    void TakeDamage(Damage damage);

    UnityEvent<Damage> OnTakeDamage { get; set; }
}

public interface IDamageDealer
{
    void DealDamage(Damage damage, IDamageable damageable);
}

[Serializable]
public class Damage
{
    public Dictionary<DamageType, float> damageData = new Dictionary<DamageType, float>();

    public Damage()
    {
        foreach (DamageType key in Enum.GetValues(typeof(DamageType)))
        {
            damageData.Add(key, 0f);
        }
    }
}

public enum DamageType
{
    Normal,
    Special,
    True
}