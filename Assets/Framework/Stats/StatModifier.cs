using UnityEngine;

namespace Framework.Stats
{
    public enum StatModifierType
    {
        Flat = 100, 
        PercentageMul = 200, 
        PercentageAdd = 300
    }
    
    [System.Serializable]
    public class StatModifier
    {
        public float value;
        public StatModifierType type;
        public int priority;
        public object source;

        public StatModifier( float value, StatModifierType type, int priority, object source)
        {
            this.value = value;
            this.type = type;
            this.priority = priority;
            this.source = source;
        }
        
        public StatModifier(float value, StatModifierType type) : this (value, type, (int)type, null) { }
        public StatModifier(float value, StatModifierType type, int order) : this (value, type, (int)type, null) { }
        public StatModifier(float value, StatModifierType type, object source) : this (value, type, (int)type, source) { }
    }
}
