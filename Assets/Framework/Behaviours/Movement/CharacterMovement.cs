using System;
using System.Collections;
using System.Collections.Generic;
using Framework.Entities;
using UnityEngine;
using UnityEngine.Serialization;
using Attribute = Framework.Entities.Attribute;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private StatsManager statsManager;
    
    [Header("Attributes")]
    private AttributeData _characterBaseMoveSpeed;
    private AttributeData _characterAdditionalMoveSpeed;

    private Camera _camera;

    private void Awake()
    {
        statsManager.TryGetAttribute(Attribute.Speed, out _characterBaseMoveSpeed);
        statsManager.TryGetAttribute(Attribute.AdditionalSpeed, out _characterAdditionalMoveSpeed);

        _camera ??= Camera.main;
    }

    public void Move(Vector3 movementDirection)
    {
        //if(statsManager.TryGetFlag(Flag.IsDashing)) return;
        
        movementDirection = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * movementDirection;
        
        characterController.Move(movementDirection.normalized * ((_characterBaseMoveSpeed.Value + _characterAdditionalMoveSpeed.Value) * Time.deltaTime));

        if (movementDirection != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, movementDirection, 0.2f);
        }
    }
    
    public void SetAdditionalMoveSpeed(float value)
    {
        _characterAdditionalMoveSpeed.Value = value;
    }
    
    public void AddAdditionalMoveSpeed(float value)
    {
        _characterAdditionalMoveSpeed.Value += value;
    }

    public void ResetAdditionalMoveSpeed()
    {
        _characterAdditionalMoveSpeed.Value = 0.0f;
    }

    public float GetCharacterMoveSpeed() => _characterBaseMoveSpeed.Value;
    public float GetCharacterAdditionalMoveSpeed() => _characterAdditionalMoveSpeed.Value;
    
}
