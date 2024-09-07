using System;
using Framework.Behaviours.Movement;
using Framework.Player;
using Project.Utils;
using UnityEngine;

namespace Framework.State_Machine
{
    public class PlayerStateMachineComponent : MonoBehaviour
    {
        private readonly StateMachine _stateMachine = new StateMachine();

        public void Initialize(PlayerController playerController)
        {
            var locomotionState = new PlayerLocomotionState(playerController);
            var dashState = new PlayerDashState(playerController);
            
            _stateMachine.AddTransition(locomotionState, dashState, new FuncPredicate(() => playerController.PlayerDash.IsDashing));
            _stateMachine.AddTransition(dashState,locomotionState, new FuncPredicate(() => !playerController.PlayerDash.IsDashing));
            
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

        protected PlayerStateMachine(PlayerController playerController)
        {
            this.playerController = playerController;
        }
        
        public override void OnEnter()
        {
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


