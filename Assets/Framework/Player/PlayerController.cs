using System;
using Framework.Behaviours.Movement;
using Framework.State_Machine;
using Project.Utils;
using UnityEngine;

namespace Framework.Player
{
    public class PlayerController : MonoBehaviour
    {
        private readonly StateMachine _stateMachine = new StateMachine();

        [field:SerializeField] public PlayerMovement PlayerMovement { get; private set; }
        [field:SerializeField] public PlayerDash PlayerDash { get; private set; }

        private void Awake()
        {
            var locomotionState = new PlayerLocomotionState(this);
            var dashState = new PlayerDashState(this);
            
            _stateMachine.AddTransition(locomotionState, dashState, new FuncPredicate(() => PlayerDash.IsDashing));
            _stateMachine.AddTransition(dashState,locomotionState, new FuncPredicate(() => !PlayerDash.IsDashing));
            
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

    public class PlayerLocomotionState : PlayerStateMachine
    {
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            playerController.PlayerMovement.Move();
            playerController.PlayerMovement.Rotate();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Enter State Locomotion");
        }

        public PlayerLocomotionState(PlayerController playerController) : base(playerController)
        {
        }
    }

    public class PlayerDashState : PlayerStateMachine
    {
        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Enter State Dash");
        }
        
        public PlayerDashState(PlayerController playerController) : base(playerController)
        {
        }
    }
}
