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
        
        public class ProjectileLauncherBuilder : Builder<ProjectileLauncher>
        {
            private Transform _bulletSpawnPoint;
            
            public ProjectileLauncherBuilder WithBulletSpawnPoint(Transform bulletSpawnPoint)
            {
                _bulletSpawnPoint = bulletSpawnPoint;

                return this;
            }

            public override ProjectileLauncher Build(Transform origin)
            {
                var build = base.Build(origin);
                build.BulletSpawnPoint = _bulletSpawnPoint ? _bulletSpawnPoint : build.BulletSpawnPoint;

                return build;
            }
        }
    }
}
