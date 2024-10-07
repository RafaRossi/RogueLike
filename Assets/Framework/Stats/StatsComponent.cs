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
            return StatsAttributes.Stats.FirstOrDefault();
        }
    }

    [Serializable]
    public class StatsAttributes
    {
        [field: SerializeReference] public List<Stat> Stats { get; private set; } = new List<Stat>();
        
        public StatsAttributes(StatsData statsData)
        {
            foreach (var statsAttribute in statsData.StatsAttributes.Stats)
            {
                Stats.Add(new Stat(statsAttribute));
            }
        }

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
