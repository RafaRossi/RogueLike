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
        

        private StatsComponent _statsComponent;
        
        public override void Initialize()
        {
            _statsComponent = origin.GetComponentInChildren<StatsComponent>();
            
            if(CanExecute()) Execute();
        }

        public override void Execute()
        {
        }

        public override void Cancel()
        {
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