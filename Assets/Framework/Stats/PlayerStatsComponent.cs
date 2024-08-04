using System.Collections.Generic;
using Framework.Stats;
using UnityEngine;

namespace Framework.Stats
{
    public class PlayerStatsComponent : StatsComponent
    {
        [field:SerializeField] public Stat DashDuration { get; set; }
    }

    public abstract class StatsComponent : MonoBehaviour
    {
        [field:SerializeField] public Stat MoveSpeed { get; set; }
        [field:SerializeField] public Stat AttackSpeed { get; set; }
    }
}
