using System;
using System.Collections;
using Framework.Player;
using Framework.State_Machine;
using Framework.Stats;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Framework.Behaviours.Movement
{
    public class PlayerDash : MonoBehaviour
    {
        [SerializeField] private Rigidbody characterRigidbody;

        [SerializeField] private float dashDuration;
        [SerializeField] private float dashForce;
        
        public bool IsDashing { get; private set; }

        private bool _canDash = true;

        private float _startDashTime;

        public UnityEvent onDashStart = new UnityEvent();
        public UnityEvent onDashFinish = new UnityEvent();
        
        public void SetCanDash(bool value) => _canDash = value;
        
        public void DashRequest()
        {
            if (_canDash && !IsDashing)
            {
                IsDashing = true;
                _startDashTime = Time.time;
                
                PerformDash();
            }
        }

        private void FixedUpdate()
        {
            if(!IsDashing) return;

            if (Time.time > _startDashTime + dashDuration)
            {
                IsDashing = false;
                onDashFinish?.Invoke();
            }
        }

        private void PerformDash()
        {
            characterRigidbody.AddForce(characterRigidbody.velocity * dashForce, ForceMode.Impulse);
            onDashStart?.Invoke();
        }
    }
    
    
    public class PlayerDashState : PlayerStateMachine
    {
        public override void OnEnter()
        {
            base.OnEnter();
        }
        
        public PlayerDashState(PlayerController playerController) : base(playerController)
        {
        }
    }
}