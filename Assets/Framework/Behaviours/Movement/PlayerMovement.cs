using System;
using Framework.Behaviours.Animations;
using Framework.Behaviours.Attack;
using Framework.Inputs;
using Framework.Player;
using Framework.State_Machine;
using Framework.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Behaviours.Movement
{
    public class PlayerMovement : BaseComponent<PlayerMovement>
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Rigidbody characterRigidbody;

        [SerializeField] private Animator animator;
        
        private Camera _camera;

        public Vector3 MovementDirection { get; private set; } = Vector3.zero;
        public Vector3 FacingDirection { get; private set; } = Vector3.zero;
        
        private float MaxSpeed => playerController.TryGetEntityOfType<StatsComponent>(out var stats) ? stats.GetStat<MoveSpeedStat>().Value : 20f;

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
            /*characterRigidbody.velocity = new Vector3(MovementDirection.x * MaxSpeed, characterRigidbody.velocity.y,
                MovementDirection.z * MaxSpeed);*/
        }
        
        public void RotateInput(Vector3 facingDirection)
        {
            FacingDirection = GetDesiredRotation(facingDirection);
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
                characterRigidbody.rotation = Quaternion.RotateTowards(characterRigidbody.rotation, desiredRotation, 1000f * Time.deltaTime);
            }
        }
        
        private Vector3 GetDesiredRotation(Vector3 mousePosition)
        {
            var ray = _camera.ScreenPointToRay(mousePosition);
            _plane.SetNormalAndPosition(Vector3.up, characterRigidbody.position);

            _plane.Raycast(ray, out var distance);
            return ray.GetPoint(distance);
        }

        private void OnAnimatorMove()
        {
            var deltaPosition = animator.deltaPosition;
            characterRigidbody.velocity += deltaPosition;
        }
    }
    
    public class PlayerLocomotionState : PlayerStateMachine
    {
        private readonly PlayerMovement _playerMovement;

        private static readonly int MovementAnimationHash = Animator.StringToHash("Movement");
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            _playerMovement.Move();
            _playerMovement.RotateWithAxis();
        }

        public PlayerLocomotionState(PlayerController playerController) : base(playerController)
        {
            var moveInputEvent = new UnityEvent<Vector3>();

            if (playerController.TryGetEntityOfType(out _playerMovement))
            {
                moveInputEvent.AddListener(_playerMovement.MoveInput);
            }
            
            if (playerController.TryGetEntityOfType(out AnimationComponent animationComponent))
            {
                moveInputEvent.AddListener((input) =>
                {
                    animationComponent.PlayAnimation(MovementAnimationHash, input.normalized.magnitude);
                });
            }

            var dashInputEvent = new UnityEvent();
            dashInputEvent.AddListener(playerController.GetEntityOfType<PlayerDash>().DashRequest);
            
            var attackInputEvent = new UnityEvent();
            if (playerController.TryGetEntityOfType<AttackController>(out var attackController))
            {
                attackInputEvent.AddListener(attackController.PrimaryAttackRequest);
            }

            playerInput = new PlayerInput.Builder()
                .WithAxisInputEvent(new AxisInputEvent(moveInputEvent))
                .WithMouseInputEvent(new MouseInputEvent(attackInputEvent))
                .WithTriggerInputEvent(new TriggerInputEvent("Dash", dashInputEvent))
                .Build();
        }
    }
}
