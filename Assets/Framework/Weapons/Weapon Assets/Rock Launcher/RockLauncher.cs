using Framework.Stats;
using UnityEngine;

namespace Framework.Weapons.Weapon_Assets.Rock_Launcher
{
    public class RockLauncher : BaseWeapon
    {
        [SerializeField] private ParticleSystem bulletsParticles;

        public StatModifier attackStatModifier;

        private Stat _attackDamage;
        private Stat _attackSpeed;

        public override void InitializeWeapon(WeaponHolder weaponHolder)
        {
            base.InitializeWeapon(weaponHolder);
            
            _attackDamage = WeaponHolder.StatsComponent.AttackDamage;
            _attackSpeed = WeaponHolder.StatsComponent.AttackSpeed;
            
            _attackDamage.AddModifier(attackStatModifier);
            
            _attackDamage.AddOnStatChangeListener(SyncParticleEffects);
            _attackSpeed.AddOnStatChangeListener(SyncParticleEffects);
        }

        public override void UnloadWeapon()
        {
            _attackDamage.RemoveModifier(attackStatModifier);
            
            _attackDamage.RemoveOnStatChangeListener(SyncParticleEffects);
            _attackSpeed.RemoveOnStatChangeListener(SyncParticleEffects);
        }

        public override void Use()
        {
            bulletsParticles.Play();
        }

        private void SyncParticleEffects()
        {
            /*var emitParam = ParticleSystem.EmitParams ;
            bulletsParticles.main..rateOverTimeMultiplier *= _attackSpeed.Value;*/
        }
    }
}
