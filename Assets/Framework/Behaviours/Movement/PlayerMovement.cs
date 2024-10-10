using System;
using System.Collections.Generic;
using Framework.Behaviours.Animations;
using Framework.Behaviours.Target;
using Framework.Entities;
using Framework.Inputs;
using Framework.Player;
using Framework.State_Machine;
using Framework.Stats;
using Project.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Framework.Behaviours.Movement
{
    public class PlayerMovement : BaseComponent<PlayerMovement>
    {
        [SerializeField] private PlayerController playerController;
        //[SerializeField] private CharacterController characterController;
        [SerializeField] private Rigidbody characterRigidbody;
        
        private Camera _camera;

        public Vector3 MovementDirection { get; private set; } = Vector3.zero;
        public Vector3 FacingDirection { get; private set; } = Vector3.zero;
        
        private float MaxSpeed => playerController.TryGetEntityOfType<StatsComponent>(out var stats) ? stats.GetStat<MoveSpeedStat>().Value : 20f;

        [SerializeField] private float acceleration = 10f;

        private Plane _plane = new Plane();

        protected override void Awake()
        {
            base.Awake();
            _camera ??= Camera.main;
        }

        public void MoveInput(Vector3 movementDirection)
        {
            MovementDirection = movementDirection.normalized;
        
            MovementDirection = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * MovementDirection;
        }

        public void Move()
        {
            //characterController.Move(MovementDirection.normalized * (playerController.StatsComponent.StatsAttributes.MoveSpeed.Value * Time.fixedDeltaTime));

            characterRigidbody.velocity = new Vector3(MovementDirection.x * MaxSpeed, characterRigidbody.velocity.y,
                MovementDirection.z * MaxSpeed);
        }
        
        public void RotateInput(Vector3 facingDirection)
        {
            FacingDirection = GetDesiredRotation(facingDirection);

            //FacingDirection = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * FacingDirection;
        }

        public void RotateWithMouse()
        {
            characterRigidbody.transform.LookAt(new Vector3(FacingDirection.x, characterRigidbody.position.y, FacingDirection.z));
        }

        public void RotateWithAxis()
        {
            if (MovementDirection != Vector3.zero)
            {
                var desiredRotation = Quaternion.LookRotation(MovementDirection, Vector3.up);
                characterRigidbody.rotation = Quaternion.RotateTowards(characterRigidbody.rotation, desiredRotation, 500f * Time.deltaTime);
            }
        }
        
        private Vector3 GetDesiredRotation(Vector3 mousePosition)
        {
            var ray = _camera.ScreenPointToRay(mousePosition);
            _plane.SetNormalAndPosition(Vector3.up, characterRigidbody.position);

            _plane.Raycast(ray, out var distance);
            return ray.GetPoint(distance);
        }

    }
    
    
    public class PlayerLocomotionState : PlayerStateMachine
    {
        private readonly PlayerMovement _playerMovement;
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            _playerMovement.Move();
            _playerMovement.RotateWithAxis();
        }

        public PlayerLocomotionState(PlayerController playerController) : base(playerController)
        {
            playerController.TryGetEntityOfType(out _playerMovement);
            
            var moveInputEvent = new UnityEvent<Vector3>();
            moveInputEvent.AddListener(_playerMovement.MoveInput);

            var dashInputEvent = new UnityEvent();
            dashInputEvent.AddListener(playerController.GetEntityOfType<PlayerDash>().DashRequest);

            var attackInputEvent = new UnityEvent<Vector3>();
            attackInputEvent.AddListener(_playerMovement.RotateInput);

            playerInput = new PlayerInput.Builder()
                .WithAxisInputEvent(new AxisInputEvent(moveInputEvent))
                .WithTriggerInputEvent(new TriggerInputEvent("Dash", dashInputEvent))
                .Build();
        }
    }

    public class PlayerAttackState : PlayerStateMachine
    {
        private readonly PlayerMovement _playerMovement;
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            _playerMovement.RotateWithMouse();
        }

        public PlayerAttackState(PlayerController playerController) : base(playerController)
        {
            playerController.TryGetEntityOfType(out _playerMovement);
        }
    }
}
