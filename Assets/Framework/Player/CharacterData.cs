using System.Collections.Generic;
using Framework.Abilities;
using Framework.Stats;
using UnityEngine;

namespace Framework.Player
{
    [CreateAssetMenu(fileName = "Character Data", menuName = "Character Data")]
    public class CharacterData : ScriptableObject
    {
        [field:SerializeField] public StatsAttributes CharacterStats { get; private set; }
        [field:SerializeField] public List<AbilityStrategy> CharacterAbilities { get; private set; } = new List<AbilityStrategy>();
    }
}
