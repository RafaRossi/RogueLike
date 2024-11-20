using System.Collections.Generic;
using Framework.Stats;
using UnityEngine;

namespace Framework.Weapons.Scripts
{
    public class MightySword : Weapon, IDamageDealer
    {
        private AttackDamageStat _attackDamageStat;
        private AttackSpecialStat _attackSpecialStat;

        private readonly PercentageStatScaling _attackDamageScaling = new(0.8f);
        
        public void DealDamage(Damage damage, IDamageable damageable)
        {
            damageable.TakeDamage(damage);
        }
        
        public override void UseWeaponPrimary()
        {
           
        }

        public override void UseWeaponSecondary()
        {
            
        }
    }
}