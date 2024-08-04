using System;
using System.Collections;
using System.Collections.Generic;
using Framework.Health_System;
using UnityEngine;
using UnityEngine.Events;

public class DamageComponent : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthComponent healthComponent;
    
    public void TakeDamage(Damage damage)
    {
        var currentHealth = healthComponent.CurrentHealth;

        currentHealth -= damage.damageAmount;
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

[Serializable]
public class Damage
{
    public float damageAmount;
}
