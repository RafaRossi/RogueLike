using UnityEngine;

namespace Framework.Stats
{
    public class PlayerStatsComponent : StatsComponent
    {
        [field:SerializeField] public Stat DashDuration { get; private set; }
    }
}
