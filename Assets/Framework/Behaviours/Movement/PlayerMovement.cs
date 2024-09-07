using System;
using Framework.Behaviours.Target;
using Framework.Entities;
using Framework.Player;
using Framework.State_Machine;
using Framework.Stats;
using Project.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Behaviours.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CharacterController characterController;
        
        private Camera _camera;

        public Vector3 MovementDirection { get; private set; } = Vector3.zero;
        private Vector3 _desiredRotation = Vector3.zero;

        private void Awake()
        {
            _camera ??= Camera.main;
        }

        public void MoveInput(Vector3 movementDirection)
        {
            MovementDirection = movementDirection;
        
            MovementDirection = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * MovementDirection;
        }

        public void Move()
        {
            characterController.Move(MovementDirection.normalized * (playerController.StatsComponent.StatsAttributes.MoveSpeed.Value * Time.fixedDeltaTime));
        }
        
        public void RotateInput(Vector3 facingDirection)
        {
            _desiredRotation = GetDesiredRotation(facingDirection);
        }

        public void Rotate()
        {
            transform.LookAt(new Vector3(_desiredRotation.x, transform.position.y, _desiredRotation.z));
        }
        
        private Vector3 GetDesiredRotation(Vector3 mousePosition)
        {
            var ray = _camera.ScreenPointToRay(mousePosition);
            
            /*if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject.GetComponent<TargetComponent>() != null)
                {
                    return hit.collider.transform.position;
                }
            }*/

            var plane = new Plane(Vector3.up, transform.position);

            plane.Raycast(ray, out var distance);
            return ray.GetPoint(distance);
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
        }

        public PlayerLocomotionState(PlayerController playerController) : base(playerController)
        {
        }
    }
}
