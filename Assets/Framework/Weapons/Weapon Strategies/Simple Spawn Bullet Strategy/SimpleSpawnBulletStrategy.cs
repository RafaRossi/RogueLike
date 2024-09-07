using Framework.Weapons.Scripts;
using Framework.Weapons.Weapon_Assets.Projectile_Launcher;
using UnityEngine;

namespace Framework.Weapons.Weapon_Strategies.Simple_Spawn_Bullet_Strategy
{
    [CreateAssetMenu(fileName = "Simple Spawn Bullet Strategy", menuName = "Weapon Strategy/Simple Spawn Bullet Strategy")]
    public class SimpleSpawnBulletStrategy : WeaponStrategy
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Vector3 spawnBulletOffset;
        

        public override void InitializeWeapon(IWeapon weapon)
        {
            
        }

        public override void UseWeapon(IWeapon weapon)
        {
            var projectileLauncher = weapon.GetWeapon<ProjectileLauncher>();
            
            if(projectileLauncher == null) return;
            
            var bullet = Instantiate(bulletPrefab, projectileLauncher.BulletSpawnPoint.position + spawnBulletOffset, projectileLauncher.BulletSpawnPoint.rotation);
            bullet.Shoot(projectileLauncher.transform, bullet.transform.forward);
        }
    }
}
