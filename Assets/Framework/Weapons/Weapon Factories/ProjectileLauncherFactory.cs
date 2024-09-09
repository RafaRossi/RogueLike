using Framework.Weapons.Scripts;
using Framework.Weapons.Weapon_Assets.Projectile_Launcher;
using UnityEngine;

namespace Framework.Weapons.Weapon_Factories
{
    [CreateAssetMenu(fileName = "Projectile Launcher Factory", menuName = "Weapon Factory/Projectile Launcher")]
    public class ProjectileLauncherFactory : WeaponFactory
    {
        public override IWeapon CreateWeapon(Transform origin, WeaponAsset weaponAsset)
        {
            return new ProjectileLauncher.ProjectileLauncherBuilder()
                .WithName("Projectile Launcher")
                .WithWeaponStrategy(weaponAsset.WeaponStrategy)
                .WithWeaponPrefab(weaponAsset.WeaponPrefab)
                .Build(origin);
        }
    }
}
