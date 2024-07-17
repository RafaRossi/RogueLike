using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CharacterMovementStats characterMovementStats;

    public void Move(Vector3 movementDirection)
    {
        characterController.Move(movementDirection.normalized * characterMovementStats.MoveSpeed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, movementDirection, 0.2f);
        }
    }

    
}
