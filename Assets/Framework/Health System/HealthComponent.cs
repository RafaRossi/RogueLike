using System;
using Framework.Entities;
using Framework.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Health_System
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private Stat maxHealth;
        [SerializeField] private Stat currentHealth;

        public UnityEvent onDie = new UnityEvent();
        public UnityEvent onHeal = new UnityEvent();
        
        public void SetCurrentHealth(float value)
        {
            currentHealth.BaseValue = value;

            if (currentHealth.Value <= 0)
            {
                Die();
            }
        }

        public float CurrentHealth => currentHealth.Value;
        
        public void SetMaxHealth(float value)
        {
            maxHealth.BaseValue = value;
        }

        private void Die()
        {
            onDie?.Invoke();
        }

        public void Heal(Heal heal)
        {
            currentHealth.BaseValue = Mathf.Clamp(currentHealth.Value + heal.healAmount, 0, maxHealth.Value);
            onHeal?.Invoke();
        }
    }
    

    [Serializable]
    public class Heal
    {
        public float healAmount;
    }
}
