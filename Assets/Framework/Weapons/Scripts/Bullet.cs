using System.Collections;
using UnityEngine;

namespace Framework.Weapons.Scripts
{
    public abstract class Bullet : MonoBehaviour
    {
        public abstract void Shoot(Transform origin, IWeapon shooterWeapon);
    }
}
