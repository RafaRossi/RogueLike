using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Stats
{
    [Serializable]
    public class Stat
    {
        [field:SerializeField] public StatID StatID { get; private set; }
        [field:SerializeField] public float BaseValue { get; set; }
        private event Action OnStatChanged = delegate {  };

        public float Value
        {
            get
            {
                if (!isDirty && Math.Abs(BaseValue - lastBaseValue) < 0.01f) return value;
                
                lastBaseValue = BaseValue;
                value = CalculateFinalValue();
                isDirty = false;

                return value;
            }
        }

        protected bool isDirty = true;
        protected float value;

        protected float lastBaseValue = float.MinValue;
        
        protected readonly List<StatModifier> statModifiers;

        public Stat()
        {
            statModifiers = new List<StatModifier>();
        }
        
        protected Stat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public Stat(StatID statID) : this()
        {
            StatID = statID;
        }

        public Stat(Stat stat) : this(stat.BaseValue)
        {
            StatID = stat.StatID;
            statModifiers = stat.statModifiers ?? new List<StatModifier>();
        }

        public void AddModifier(StatModifier statModifier)
        {
            isDirty = true;
            
            statModifiers.Add(statModifier);
            statModifiers.Sort(ComparePriority);
        }

        protected int ComparePriority(StatModifier a, StatModifier b)
        {
            if (a.priority < b.priority) return -1;
            return a.priority > b.priority ? 1 : 0;
        }

        public bool RemoveModifier(StatModifier statModifier)
        {
            if (statModifiers.Remove(statModifier))
            {
                isDirty = true;
            }

            return isDirty;
        }

        protected float CalculateFinalValue()
        {
            var finalValue = BaseValue;
            var sumPercentAdd = 0.0f;

            for (var i = 0; i < statModifiers.Count;  i++)
            {
                switch (statModifiers[i].type)
                {
                    case StatModifierType.Flat:
                        finalValue += statModifiers[i].value;
                        break;
                    
                    case StatModifierType.PercentageMul:
                        finalValue *= 1 + statModifiers[i].value;
                        if (i + 1 >= statModifiers.Count ||
                            statModifiers[i + 1].type != StatModifierType.PercentageAdd)
                        {
                            finalValue *= 1 + sumPercentAdd;
                            sumPercentAdd = 0;
                        }
                        break;
                    
                    case StatModifierType.PercentageAdd:
                        sumPercentAdd += statModifiers[i].value;
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            OnStatChanged?.Invoke();
            
            return (float)Math.Round(finalValue, 4);
        }

        public void AddOnStatChangeListener(Action onStatChanged)
        {
            OnStatChanged += onStatChanged;
        }
        
        public void RemoveOnStatChangeListener(Action onStatChanged)
        {
            OnStatChanged -= onStatChanged;
        }
    }
}
