/*public enum StatID
{
    MoveSpeed,
        
    DashDuration,
        
    AttackDamage,
    AttackSpeed,
        
    Defense,
        
    ProjectileCount,
}*/

using UnityEngine;

namespace Framework.Stats
{
    public class StatID : ScriptableObject
    {
        [field:SerializeField] public Sprite Icon { get; private set; } 
    }
}