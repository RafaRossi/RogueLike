using System;
using System.Collections;
using Framework.Player;
using Framework.State_Machine;
using Framework.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework.Behaviours.Movement
{
    public class PlayerDash : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        
        [SerializeField] private StatModifier characterDashSpeed;
        [SerializeField] private float dashDuration;
        
        public bool IsDashing { get; private set; }

        private bool _canDash = true;

        private float _startDashTime;
        
        public void SetCanDash(bool value) => _canDash = value;
        
        public void DashRequest()
        {
            if (_canDash && !IsDashing)
            {
                IsDashing = true;
                _startDashTime = Time.time;
                
                playerController.StatsComponent.StatsAttributes.MoveSpeed.AddModifier(characterDashSpeed);
            }
        }

        private void FixedUpdate()
        {
            if(!IsDashing) return;

            if (Time.time < _startDashTime + dashDuration)
            {
                playerController.PlayerMovement.Move();
            }
            else
            {
                playerController.StatsComponent.StatsAttributes.MoveSpeed.RemoveModifier(characterDashSpeed);
                IsDashing = false;
            }
        }

        /*private void PerformDash()
        {

            var startTime = Time.time;
            
            playerController.StatsComponent.StatsAttributes.MoveSpeed.AddModifier(characterDashSpeed);

            while (Time.time < startTime + dashDuration/*playerStatsComponent.DashDuration.Value)
            {
                playerController.PlayerMovement.Move();
                
                yield return null;
            }
            
            playerController.StatsComponent.StatsAttributes.MoveSpeed.RemoveModifier(characterDashSpeed);

            IsDashing = false;
        }*/
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