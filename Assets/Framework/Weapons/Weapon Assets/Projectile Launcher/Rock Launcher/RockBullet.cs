using Framework.Weapons.Scripts;
using UnityEngine;

namespace Framework.Weapons.Weapon_Assets.Projectile_Launcher.Rock_Launcher
{
    public class RockBullet : Bullet
    {
        [SerializeField] private Rigidbody rb;
        
        [SerializeField] private float bulletLifeDuration;
        [SerializeField] private float traversalSpeed;

        public override void Shoot(Transform origin, Vector3 direction)
        {
            rb.AddForce(direction * traversalSpeed, ForceMode.Impulse);
            Destroy(gameObject, bulletLifeDuration);
        }
    }
}
