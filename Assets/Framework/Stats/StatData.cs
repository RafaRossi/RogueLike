using UnityEngine;

namespace Framework.Stats
{
    [CreateAssetMenu(menuName = "Create StatsData", fileName = "StatsData", order = 0)]
    public class StatsData : ScriptableObject
    {
        [field:SerializeField] public StatsAttributes StatsAttributes { get; private set; }
    }
}
