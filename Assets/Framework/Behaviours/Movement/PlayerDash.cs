using System.Collections;
using Framework.Stats;
using UnityEngine;

namespace Framework.Behaviours.Movement
{
    public class PlayerDash : MonoBehaviour
    {
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerStatsComponent playerStatsComponent;
        
        [SerializeField] private StatModifier characterDashSpeed;
        [SerializeField] private float dashDuration;
        
        public bool IsDashing { get; private set; }

        private bool _canDash = true;
        
        public void SetCanDash(bool value) => _canDash = value;
        
        public void DashRequest()
        {
            if (_canDash && !IsDashing)
            {
                StartCoroutine(PerformDash());
            }
        }
        
        private IEnumerator PerformDash()
        {
            IsDashing = true;
            
            var startTime = Time.time;
            
            playerStatsComponent.StatsAttributes.MoveSpeed.AddModifier(characterDashSpeed);

            while (Time.time < startTime + dashDuration/*playerStatsComponent.DashDuration.Value*/)
            {
                playerMovement.Move();
                
                yield return null;
            }
            
            playerStatsComponent.StatsAttributes.MoveSpeed.RemoveModifier(characterDashSpeed);

            IsDashing = false;
        }
    }
}