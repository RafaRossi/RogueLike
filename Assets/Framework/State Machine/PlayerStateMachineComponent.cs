using System;
using Framework.Behaviours.Animations;
using Framework.Behaviours.Attack;
using Framework.Behaviours.Movement;
using Framework.Inputs;
using Framework.Player;
using Project.Utils;
using UnityEngine;

namespace Framework.State_Machine
{
    public class PlayerStateMachineComponent : BaseComponent<PlayerStateMachineComponent>
    {
        private readonly StateMachine _stateMachine = new StateMachine();

        public void Initialize(PlayerController playerController)
        {
            var locomotionState = new PlayerLocomotionState(playerController);
            var dashState = new PlayerDashState(playerController);
            var attackState = new PlayerAttackState(playerController);

            _stateMachine.AddTransition(locomotionState, dashState, new FuncPredicate(() => dashState.PlayerDash.IsDashing));
            _stateMachine.AddTransition(dashState,locomotionState, new FuncPredicate(() => !dashState.PlayerDash.IsDashing));
            _stateMachine.AddTransition(locomotionState, attackState, new FuncPredicate(() => attackState.IsAttacking));
            _stateMachine.AddTransition(attackState, locomotionState, new FuncPredicate(() => !attackState.IsAttacking));
            
            _stateMachine.SetState(locomotionState);
        }
        
        private void Update()
        {
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
    }
    
    public abstract class PlayerStateMachine : BaseState
    {
        protected readonly PlayerController playerController;
        protected PlayerInput playerInput = new PlayerInput();

        protected PlayerStateMachine(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public PlayerStateMachine WithPlayerInput(PlayerInput newPlayerInput)
        {
            playerInput = newPlayerInput;

            return this;
        }
        
        public override void OnEnter()
        {
            if(playerController.TryGetEntityOfType<PlayerInputComponent>(out var playerInputComponent))
            {
                playerInputComponent.ChangeInput(playerInput);
            }
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {

        }

        public override void OnExit()
        {
            
        }
    }
}


