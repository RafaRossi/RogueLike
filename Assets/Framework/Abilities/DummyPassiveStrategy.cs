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
        private StatModifier _attackDamage = new StatModifier(50, StatModifierType.Flat);
        private StatModifier _attackSpecial = new StatModifier(50, StatModifierType.Flat);

        private StatsComponent _statsComponent;
        
        public override void Initialize()
        {
            _statsComponent = origin.GetComponentInChildren<StatsComponent>();
            
            if(CanExecute()) Execute();
        }

        public override void Execute()
        {
            _statsComponent.StatsAttributes.AttackDamage.AddModifier(_attackDamage);
            _statsComponent.StatsAttributes.AttackSpecial.AddModifier(_attackSpecial);
        }

        public override void Cancel()
        {
            _statsComponent.StatsAttributes.AttackDamage.RemoveModifier(_attackDamage);
            _statsComponent.StatsAttributes.AttackSpecial.RemoveModifier(_attackSpecial);
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