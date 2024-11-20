using System;
using Framework.Abilities;
using Framework.Behaviours.Animations;
using Framework.Behaviours.Movement;
using Framework.Entities;
using Framework.Inputs;
using Framework.State_Machine;
using Framework.Stats;
using Project.Utils;
using UnityEngine;

namespace Framework.Player
{
    public class PlayerController : ComponentController
    {
        private void Start()
        {
            if(TryGetEntityOfType<PlayerStateMachineComponent>(out var stateMachine))
            {
                stateMachine.Initialize(this);
            }
            
            if(TryGetEntityOfType<StatsComponent>(out var statsComponent))
            {
                statsComponent.Initialize(CharacterData.CharacterStats);
            }

            if (TryGetEntityOfType<AbilityController>(out var abilityController))
            {
                abilityController.InitializeAbilities(CharacterData.CharacterAbilities);
            }
        }
    }
}
