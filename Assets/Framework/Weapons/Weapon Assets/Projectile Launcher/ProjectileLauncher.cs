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
            
        }
    }
}
