using Framework.Behaviours.Target;
using Framework.Stats;
using Framework.Weapons.Scripts;
using Project.Utils;
using UnityEngine;

namespace Framework.Weapons.Weapon_Assets.Projectile_Launcher
{
    public class ProjectileLauncher : Weapon
    {
        [field:SerializeField] public Transform BulletSpawnPoint { get; private set; }
        
        public StatModifier attackStatModifier;

        private Stat _attackDamage;
        private Stat _attackSpeed;

        private Camera _camera;

        private WeaponHolder _weaponHolder;

        private void Awake()
        {
            _camera ??= Camera.main;
        }

        public override void UseWeapon(WeaponHolder weaponHolder)
        {
            _weaponHolder = weaponHolder;
            
            WeaponStrategy.UseWeapon(this);
        }

        public class ProjectileLauncherBuilder : Builder<ProjectileLauncher>
        {
            
        }
    }
}
