using UnityEngine;

namespace Framework.Abilities
{
    public abstract class AbilityStrategy : ScriptableObject
    {
        [field:SerializeField] public AbilityData AbilityData { get; private set; }
        public abstract IAbility BuildAbility(AbilityController origin);
    }

    [System.Serializable]
    public class AbilityData
    {
        [field:SerializeField] public string AbilityName { get; private set; }
        [field:SerializeField] public string AbilityDescription { get; private set; }
    }
}