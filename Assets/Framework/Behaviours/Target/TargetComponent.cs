using UnityEngine;

namespace Framework.Behaviours.Target
{
    public class TargetComponent : MonoBehaviour, ITarget
    {
        [field: SerializeField] public bool CanBeTarget { get; set; } = true;
    }

    public interface ITarget
    {
        bool CanBeTarget { get; set; }
    }
}
