using Framework.Player;
using Framework.State_Machine;
using Framework.Stats;
using Project.Utils;
using UnityEngine;

namespace Framework.Abilities
{
    public abstract class Ability : IAbility
    {
        public AbilityController origin;
        protected readonly StateMachine stateMachine = new StateMachine();
        
        public abstract void Execute();
        public abstract void Cancel();
        public abstract void Initialize();
        public abstract bool CanExecute();
        
        public abstract class Builder<T> where T : Ability, new()
        {
            protected AbilityController origin;
            
            public Builder<T> WithOrigin(AbilityController origin)
            {
                this.origin = origin;
                return this;
            }

            public virtual T Build()
            {
                return new T
                {
                    origin = origin
                };
            }
        }
    }
    
    public abstract class PassiveAbility : Ability
    {
        
    }
}