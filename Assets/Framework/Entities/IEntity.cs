using Framework.Abilities;
using Framework.Behaviours.Animations;
using Framework.Player;
using Framework.Stats;
using UnityEngine;

namespace Framework.Entities
{
    public interface IEntity
    {
        
    }

    public abstract class EntityController : MonoBehaviour, IEntity
    {
        [field:SerializeField] public CharacterData CharacterData { get; private set; }
        [field:SerializeField] public AnimationComponent AnimationComponent { get; private set; }
        [field:SerializeField] public StatsComponent StatsComponent { get; private set; }
        [field:SerializeField] public AbilityController AbilityController { get; private set; }
    }
}