using Framework.Weapons.Scripts;
using UnityEngine;

namespace Framework.Weapons.Weapon_Strategies.Simple_Spawn_Bullet_Strategy
{
    [CreateAssetMenu(fileName = "Simple Spawn Bullet Strategy", menuName = "Weapon Strategy/Simple Spawn Bullet Strategy")]
    public class SimpleSpawnBulletStrategy : WeaponStrategy
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Vector3 spawnBulletOffset;

        public override void UseWeapon(Transform origin, IWeapon weapon)
        {
            var bullet = Instantiate(bulletPrefab, origin.position + spawnBulletOffset, origin.rotation);
            bullet.Shoot(origin, weapon);
        }
    }
}
