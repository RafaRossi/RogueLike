using System;
using UnityEngine;

namespace Framework.Stats
{
    [CreateAssetMenu(menuName = "Stats Data", fileName = "Stats Data", order = 0)]
    public class StatsData : ScriptableObject
    {
        [field:SerializeField] public StatsAttributes StatsAttributes { get; private set; }
    }

    [Serializable]
    public class HealthStat : Stat
    {
        public HealthStat()
        {
            name = "Health";
        }
    }

    [Serializable]
    public class MoveSpeedStat : Stat
    {
        public MoveSpeedStat()
        {
            name = "Move Speed";
        }
    }

    [Serializable]
    public class AttackSpeedStat : Stat
    {
        public AttackSpeedStat()
        {
            name = "Attack Speed";
        }
    }

    [Serializable]
    public class AttackDamageStat : Stat
    {
        public AttackDamageStat()
        {
            name = "Attack Damage";
        }
    }

    [Serializable]
    public class AttackSpecialStat : Stat
    {
        public AttackSpecialStat()
        {
            name = "Special Attack";
        }
    }

    [Serializable]
    public class DefenseStat : Stat
    {
        public DefenseStat()
        {
            name = "Defense";
        }
    }

    [Serializable]
    public class DefenseSpecialStat : Stat
    {
        public DefenseSpecialStat()
        {
            name = "Special Defense";
        }
    }
}
