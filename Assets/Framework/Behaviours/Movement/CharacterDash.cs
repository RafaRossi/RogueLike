using System;
using System.Collections;
using System.Collections.Generic;
using Framework.Entities;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterDash : MonoBehaviour
{
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private StatsManager statsManager;
    
    [SerializeField] private float characterDashSpeed;
    [SerializeField] private float characterDashDuration;

    public void Dash()
    {
        StartCoroutine(PerformDash());
        
        IEnumerator PerformDash()
        {
            if (statsManager.TryGetFlag(Flag.IsDashing)) yield break;

            statsManager.AddFlag(Flag.IsDashing);
        
            var startTime = Time.time;
            
            characterMovement.AddAdditionalMoveSpeed(characterDashSpeed);

            while (Time.time < startTime + characterDashDuration)
            {
                yield return null;
            }

            characterMovement.AddAdditionalMoveSpeed(-characterDashSpeed);
            statsManager.RemoveFlag(Flag.IsDashing);
        }
    }
}
