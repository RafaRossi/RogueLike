using System.Collections.Generic;
using Framework.Stats;
using UnityEngine;

namespace Framework.Stats
{
    public class StatsComponent : MonoBehaviour
    {
        /*private readonly Dictionary<StatID, Stat> _currentStats = new Dictionary<StatID, Stat>();

        [SerializeField] private List<Stat> stats = new List<Stat>();
        
        public void AddStat(Stat stat)
        {
            _currentStats.TryAdd(stat.StatID, stat);
        }

        public bool RemoveStat(Stat stat)
        {
            return _currentStats.Remove(stat.StatID);
        }

        public bool TryGetStat(StatID statID, out Stat stat)
        {
            return _currentStats.TryGetValue(statID, out stat);
        }*/
        [field:SerializeField] public Stat Health { get; private set; }
        
        [field:SerializeField] public Stat MoveSpeed { get; private set; }
        
        [field:SerializeField] public Stat AttackDamage { get; private set; }
        [field:SerializeField] public Stat AttackSpeed { get; private set; }
        
        [field:SerializeField] public Stat Defense { get; private set; }
    }
}
