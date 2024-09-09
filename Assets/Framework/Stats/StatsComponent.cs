using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Stats
{
    public class StatsComponent : MonoBehaviour
    {
        public StatsAttributes StatsAttributes { get; private set; }

        public void Initialize(StatsData statsData)
        {
            StatsAttributes = new StatsAttributes(statsData);
        }

        public Stat GetStat(StatID statID)
        {
            return StatsAttributes.Stats.FirstOrDefault(stat => stat.StatID == statID);
        }
    }

    [Serializable]
    public class StatsAttributes
    {
        /*[field: SerializeField] public Stat MaxHealth { get; private set; }
        [field:SerializeField] public Stat MoveSpeed { get; private set; }
        [field:SerializeField] public Stat MaxSpeed { get; private set; }
        
        [field:SerializeField] public Stat AttackDamage { get; private set; }
        [field:SerializeField] public Stat AttackSpeed { get; private set; }
        [field:SerializeField] public Stat AttackSpecial { get; private set; }
        
        [field:SerializeField] public Stat Defense { get; private set; }

        private readonly List<Stat> _allStats = new List<Stat>();*/

        [field: SerializeField] public List<Stat> Stats { get; private set; } = new List<Stat>();
        
        public StatsAttributes(StatsData statsData)
        {
            /*MaxHealth = new Stat(statsData.StatsAttributes.MaxHealth);
            
            MoveSpeed = new Stat(statsData.StatsAttributes.MoveSpeed);
            MaxSpeed = new Stat(statsData.StatsAttributes.MaxSpeed);
            
            AttackDamage = new Stat(statsData.StatsAttributes.AttackDamage);
            AttackSpeed = new Stat(statsData.StatsAttributes.AttackSpeed);
            AttackSpecial = new Stat(statsData.StatsAttributes.AttackSpecial);
            
            Defense = new Stat(statsData.StatsAttributes.Defense);*/
            
            //_allStats.AddRange( new List<Stat>{ MaxHealth, MoveSpeed, MaxSpeed, AttackDamage, AttackSpeed, AttackSpecial, Defense });

            foreach (var statsAttribute in statsData.StatsAttributes.Stats)
            {
                Stats.Add(new Stat(statsAttribute));
            }
        }

        public void AddStatsModifiers(List<StatModifier> statModifiers)
        {
            foreach (var allStat in Stats)
            {
                foreach (var statModifier in statModifiers.Where(statModifier => allStat.StatID == statModifier.statID))
                {
                    allStat.AddModifier(statModifier);
                }
            }
        }

        public void RemoveStatsModifiers(List<StatModifier> statModifiers)
        {
            foreach (var allStat in Stats)
            {
                foreach (var statModifier in statModifiers.Where(statModifier => allStat.StatID == statModifier.statID))
                {
                    allStat.RemoveModifier(statModifier);
                }
            }
        }
    }
}
