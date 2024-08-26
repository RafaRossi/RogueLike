using Framework.Weapons.Scripts;
using UnityEngine;

namespace Framework.Weapons.Weapon_Assets.Rock_Launcher
{
    public class RockBullet : Bullet
    {
        [SerializeField] private Rigidbody rb;
        
        [SerializeField] private float bulletLifeDuration;
        [SerializeField] private float traversalSpeed;

        public override void Shoot(Transform origin, IWeapon shooterWeapon)
        {
            rb.AddForce(transform.forward * traversalSpeed, ForceMode.Impulse);
            Destroy(gameObject, bulletLifeDuration);
        }
    }
}
