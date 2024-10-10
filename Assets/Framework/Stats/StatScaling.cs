using System;
using UnityEngine;

namespace Framework.Stats
{
    public interface IStatScalingStrategy
    {
        float GetScalingValue(Stat stat);
    }

    [Serializable]
    public class PercentageStatScaling : IStatScalingStrategy
    {
        [SerializeField] [Range(0f, 1f)] private float scalingFactor;

        public PercentageStatScaling(float scalingFactor)
        {
            this.scalingFactor = scalingFactor;
        }
        
        public float GetScalingValue(Stat stat)
        {
            return (stat.Value * scalingFactor);
        }
    }
}
