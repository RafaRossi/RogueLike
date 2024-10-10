using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Behaviours.Animations;
using Framework.Entities;
using Framework.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Stats
{
    public class StatsComponent : BaseComponent<StatsComponent>
    {
        [field:SerializeField] public StatsAttributes StatsAttributes { get; private set; }

        /*public void Initialize(StatsData statsData)
        {
            StatsAttributes = new StatsAttributes(statsData);
        }*/

        public void Initialize(StatsAttributes statsAttributes)
        {
            StatsAttributes = statsAttributes;
        }

        public Stat GetStat<T>() where T : Stat, new()
        {
            foreach (var stat in StatsAttributes.Stats.Where(stat => stat.GetType() == typeof(T)))
            {
                return stat;
            }

            return new T();
        }
    }

    [Serializable]
    public class StatsAttributes
    {
        [field: SerializeReference] public List<Stat> Stats { get; private set; } = new List<Stat>();
        
        public StatsAttributes()
        {
            Stats = new List<Stat>
            {
                new DefenseStat(),
                new HealthStat(),
                new AttackDamageStat(),
                new AttackSpecialStat(),
                new AttackSpeedStat(),
                new DefenseSpecialStat(),
                new MoveSpeedStat()
            };
        }

        public StatsAttributes(List<Stat> stats)
        {
            foreach (var stat in stats)
            {
                Stats.Add(stat);
            }
        }
        
       /* public StatsAttributes(StatsData statsData)
        {
            foreach (var statsAttribute in statsData.StatsAttributes.Stats)
            {
                Stats.Add(new Stat(statsAttribute));
            }
        }*/

        public void AddStatsModifiers(List<StatModifier> statModifiers)
        {
            foreach (var allStat in Stats)
            {
                /*foreach (var statModifier in statModifiers.Where(statModifier => allStat.StatID == statModifier.statID))
                {
                    allStat.AddModifier(statModifier);
                }*/
            }
        }

        public void RemoveStatsModifiers(List<StatModifier> statModifiers)
        {
            foreach (var allStat in Stats)
            {
                /*foreach (var statModifier in statModifiers.Where(statModifier => allStat.StatID == statModifier.statID))
                {
                    allStat.RemoveModifier(statModifier);
                }*/
            }
        }
    }
}
