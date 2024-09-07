using System;
using Framework.Abilities;
using Framework.Behaviours.Animations;
using Framework.Behaviours.Movement;
using Framework.Entities;
using Framework.State_Machine;
using Framework.Stats;
using Project.Utils;
using UnityEngine;

namespace Framework.Player
{
    public class PlayerController : EntityController
    {
        [field:SerializeField] public PlayerStateMachineComponent StateMachineComponent { get; private set; }
        [field:SerializeField] public PlayerMovement PlayerMovement { get; private set; }
        [field:SerializeField] public PlayerDash PlayerDash { get; private set; }

        private void Awake()
        {
            StateMachineComponent.Initialize(this);
            
            StatsComponent.Initialize(CharacterData.CharacterStats);
            AbilityController.InitializeAbilities(CharacterData.CharacterAbilities);
        }
    }
}
