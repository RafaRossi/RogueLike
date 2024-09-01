using System.Collections;
using Framework.Behaviours.Target;
using Framework.Entities;
using Framework.Stats;
using Project.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Behaviours.Movement
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private CharacterFlags characterFlags;
        [SerializeField] private PlayerStatsComponent playerStatsComponent;
        
        [SerializeField] private StatModifier characterDashSpeed;

        private Stat _moveSpeed;
        private Stat _dashDuration;
        
        private Camera _camera;

        private Vector3 _movementDirection = Vector3.zero;
        private Vector3 _desiredRotation = Vector3.zero;

        private void Awake()
        {
            _camera ??= Camera.main;

            _moveSpeed = playerStatsComponent.MoveSpeed;
            _dashDuration = playerStatsComponent.DashDuration;
        }

        public void MoveInput(Vector3 movementDirection)
        {
            if(characterFlags.TryGetFlag(Flag.IsDashing)) return;
        
            _movementDirection = movementDirection;
        
            _movementDirection = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * _movementDirection;
        }

        public void Move()
        {
            characterController.Move(_movementDirection.normalized * (_moveSpeed.Value * Time.deltaTime));
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
        
        public void Dash()
        {
            StartCoroutine(PerformDash());
            return;

            IEnumerator PerformDash()
            {
                if (characterFlags.TryGetFlag(Flag.IsDashing)) yield break;

                characterFlags.AddFlag(Flag.IsDashing);
        
                var startTime = Time.time;
                
                _moveSpeed.AddModifier(characterDashSpeed);

                while (Time.time < startTime + _dashDuration.Value)
                {
                    characterController.Move(_movementDirection.normalized * (_moveSpeed.Value * Time.deltaTime));
                
                    yield return null;
                }

                _moveSpeed.RemoveModifier(characterDashSpeed);
                characterFlags.RemoveFlag(Flag.IsDashing);
            }
        }

        public Stat GetCharacterMoveSpeed() => _moveSpeed;

    }
}
