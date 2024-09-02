using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Abilities
{
    public class AbilityController : MonoBehaviour
    {
        private readonly Dictionary<AbilityStrategy, IAbility> _abilities = new Dictionary<AbilityStrategy, IAbility>();
        
        public void InitializeAbilities(List<AbilityStrategy> abilitiesData)
        {
            _abilities.Clear();
            
            foreach (var abilityData in abilitiesData)
            {
                AddAbility(abilityData);
            }
        }

        public void AddAbility(AbilityStrategy abilityData)
        {
            if(_abilities.ContainsKey(abilityData)) return;
            
            var ability = abilityData.BuildAbility(this);
            ability.Initialize();
            _abilities.Add(abilityData, ability);
        }

        public void RemoveAbility(AbilityStrategy abilityData)
        {
            if (!_abilities.TryGetValue(abilityData, out var ability)) return;
            
            ability.Cancel();

            _abilities.Remove(abilityData);
        }
    }
}