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
        [SerializeField] private StatsData statsData;
        public StatsAttributes StatsAttributes { get; private set; }

        private void Awake()
        {
            StatsAttributes = new StatsAttributes(statsData);
        }
    }

    [Serializable]
    public class StatsAttributes
    {
        [field:SerializeField] public Stat MaxHealth { get; private set; }
        [field:SerializeField] public Stat MoveSpeed { get; private set; }
        
        [field:SerializeField] public Stat AttackDamage { get; private set; }
        [field:SerializeField] public Stat AttackSpeed { get; private set; }
        [field:SerializeField] public Stat AttackSpecial { get; private set; }
        
        [field:SerializeField] public Stat Defense { get; private set; }
        
        public StatsAttributes(StatsData statsData)
        {
            MaxHealth = new Stat(statsData.StatsAttributes.MaxHealth);
            MoveSpeed = new Stat(statsData.StatsAttributes.MoveSpeed);
            
            AttackDamage = new Stat(statsData.StatsAttributes.AttackDamage);
            AttackSpeed = new Stat(statsData.StatsAttributes.AttackSpeed);
            AttackSpecial = new Stat(statsData.StatsAttributes.AttackSpecial);
            
            Defense = new Stat(statsData.StatsAttributes.Defense);
        }
    }
}
