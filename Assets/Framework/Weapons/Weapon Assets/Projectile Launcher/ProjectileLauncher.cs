using Framework.Behaviours.Target;
using Framework.Stats;
using Framework.Weapons.Scripts;
using Project.Utils;
using UnityEngine;

namespace Framework.Weapons.Weapon_Assets.Projectile_Launcher
{
    public class ProjectileLauncher : Weapon
    {
        [SerializeField] private Transform bulletSpawnPoint;
        public StatModifier attackStatModifier;

        private Stat _attackDamage;
        private Stat _attackSpeed;

        private Camera _camera;

        private void Awake()
        {
            _camera ??= Camera.main;
        }

        public override void UseWeapon()
        {
            //bulletSpawnPoint.rotation =  Quaternion.Euler(new Vector3(0f,180 - GetAimDirection(),0f));
            WeaponStrategy.UseWeapon(bulletSpawnPoint, this);
        }

        private float GetAimDirection()
        {
            var positionOnScreen = _camera.WorldToViewportPoint (transform.position);
            var mouseOnScreen = (Vector2)_camera.ScreenToViewportPoint(TryGetTarget());

            return Extensions.AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen) + _camera.transform.eulerAngles.y;
        }

        private Vector2 TryGetTarget()
        {
            //var mousePosition = (Vector2)_camera.ScreenToViewportPoint(Input.mousePosition);
            var mousePosition = Input.mousePosition;

            if (!Physics.SphereCast(_camera.ScreenPointToRay(Input.mousePosition), 2f, out var hitInfo))
            {
                return mousePosition;
            }
            
            if (!hitInfo.collider.TryGetComponent<TargetComponent>(out var target)) return mousePosition;
            return target.CanBeTarget ? _camera.WorldToScreenPoint(target.transform.position) : mousePosition;
        }

        public class ProjectileLauncherBuilder : Builder
        {
            
        }
    }
}
