using System;
using System.Collections.Generic;
using Framework.Behaviours.Animations;
using Framework.Behaviours.Movement;
using Framework.Inputs;
using Framework.Player;
using Framework.State_Machine;
using Framework.Weapons.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Framework.Behaviours.Attack
{
    public class AttackController : BaseComponent<AttackController>
    {
        [SerializeField] private AnimationComponent animationComponent;
        [field:SerializeField] public WeaponHolder WeaponHolder { get; private set; }
        
        public List<AttackData> attacksData = new List<AttackData>();

        private int _currentAttackData = -1;

        public float LastAttackRequest { get; private set; }

        private bool _canAttack = true;


        public bool IsAttacking { get; set; }

        private void OnEnable()
        {
            WeaponHolder.onEquipWeapon.AddListener(OnEquipWeapon);
        }

        private void OnDisable()
        {
            WeaponHolder.onEquipWeapon.RemoveListener(OnEquipWeapon);
        }

        private void OnEquipWeapon(IWeapon weapon)
        {
            attacksData = new List<AttackData>();
            
            foreach (var attackData in WeaponHolder.GetWeaponAsset().attackData)
            {
                attacksData.Add(new AttackData(attackData));
            }
        }

        public void PrimaryAttackRequest()
        {
            if(_canAttack) WeaponHolder.UseWeaponPrimaryRequest();
            /*if (_currentAttackData < 0)
            {
                _currentAttackData++;
            }
            
            if (_currentAttackData >= 0 && _currentAttackData < attacksData.Count)
            {
                if (Time.time >= attacksData[_currentAttackData].attackInputCooldown + LastAttackRequest)
                {
                    WeaponHolder.UseWeaponPrimaryRequest();

                    _currentAttackData++;

                    if (_currentAttackData >= attacksData.Count)
                    {
                        _currentAttackData = -1;
                    }

                    LastAttackRequest = Time.time;
                }
            }*/
        }

        public void SecondaryAttackRequest()
        {
            if(_canAttack) WeaponHolder.UseWeaponSecondaryRequest();
        }

        public AttackData GetCurrentAttack()
        {
            return _currentAttackData > -1 ? attacksData[_currentAttackData] : null;
        }
    }
    
    [Serializable]
    public class AttackData
    {
        public float attackInputCooldown;

        public float attacktime;
        //public AnimationClip attackAnimation;

        public AttackData(AttackData attackData)
        {
            attackInputCooldown = attackData.attackInputCooldown;
            attacktime = attackData.attacktime;
        }
    }

    public class PlayerAttackState : PlayerStateMachine
    {
        private readonly PlayerMovement _playerMovement;
        private readonly AttackController _attackController;
        
        public bool IsAttacking => _attackController.IsAttacking;

        private void CorrectAnimationMovement(Vector3 deltaPosition)
        {
            _playerMovement.Move(deltaPosition);
        }
        
        public PlayerAttackState(PlayerController playerController) : base(playerController)
        {
            var attackInputAxisEvent = new UnityEvent<Vector3>();
            
            if (playerController.TryGetEntityOfType(out _playerMovement))
            {
                attackInputAxisEvent.AddListener(_playerMovement.RotateInput);
            }

            var attackInputEvent = new UnityEvent();

            if (playerController.TryGetEntityOfType(out _attackController))
            {
                /*attackInputEvent.AddListener(_playerMovement.RotateWithMouse);
                attackInputEvent.AddListener(_attackController.PrimaryAttackRequest);*/
            }
            
            if (playerController.TryGetEntityOfType(out AnimationComponent animationComponent))
            {
                animationComponent.RegisterAnimatorMove(CorrectAnimationMovement);
            }

            playerInput = new PlayerInput.Builder()
                .WithMouseInputEvent(new MouseInputEvent(attackInputAxisEvent))
                .WithMouseInputEvent(new MouseInputEvent(attackInputEvent))
                .Build();
        }
    }
}