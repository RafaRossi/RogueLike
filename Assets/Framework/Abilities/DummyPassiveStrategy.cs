using Framework.Stats;
using UnityEngine;

namespace Framework.Abilities
{
    [CreateAssetMenu(fileName = "Dummy Passive", menuName = "Abilities/Passives/Dummy Passive")]
    public class DummyPassiveStrategy : AbilityStrategy
    {
        public override IAbility BuildAbility(AbilityController origin)
        {
            return new DummyPassive.DummyPassiveBuilder().WithOrigin(origin).Build();
        }
    }
    
    public class DummyPassive : PassiveAbility
    {
        private readonly StatModifier _attackDamage = new StatModifier(StatID.AttackDamage,50, StatModifierType.Flat);
        private readonly StatModifier _attackSpecial = new StatModifier(StatID.AttackSpecial, 50, StatModifierType.Flat);

        private StatsComponent _statsComponent;
        
        public override void Initialize()
        {
            _statsComponent = origin.GetComponentInChildren<StatsComponent>();
            
            if(CanExecute()) Execute();
        }

        public override void Execute()
        {
            _statsComponent.GetStat(StatID.AttackDamage)?.AddModifier(_attackDamage);
            _statsComponent.GetStat(StatID.AttackSpecial)?.AddModifier(_attackSpecial);
        }

        public override void Cancel()
        {
            _statsComponent.GetStat(StatID.AttackDamage)?.RemoveModifier(_attackDamage);
            _statsComponent.GetStat(StatID.AttackSpecial)?.RemoveModifier(_attackSpecial);
        }

        public override bool CanExecute()
        {
            return true;
        }

        public class DummyPassiveBuilder : Builder<DummyPassive>
        {
            
        }
    }
}