using UnityEngine;

namespace Framework.Weapons.Scripts
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public string Name { get; set; }
        
        public WeaponHolder WeaponHolder { get; set; }

        public virtual void UseWeaponPrimary()
        {
            //WeaponHolder.AttackController.animationComponent.PlayAnimationCrossFade(Animator.StringToHash("Attack"));
            WeaponHolder.onUsePrimaryWeapon?.Invoke();
        }

        public virtual void UseWeaponSecondary()
        {
            
        }

        public virtual Weapon Initialize(WeaponHolder weaponHolder)
        {
            WeaponHolder = weaponHolder;
            return this;
        }
    }
}
