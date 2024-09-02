using UnityEngine;

namespace Framework.Stats
{
    [CreateAssetMenu(menuName = "Stats Data", fileName = "Stats Data", order = 0)]
    public class StatsData : ScriptableObject
    {
        [field:SerializeField] public StatsAttributes StatsAttributes { get; private set; }
    }
}
