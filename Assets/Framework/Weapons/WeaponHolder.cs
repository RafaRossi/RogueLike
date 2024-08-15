using Framework.Stats;
using UnityEngine;

namespace Framework.Weapons
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private BaseWeapon currentWeapon;
        public StatsComponent StatsComponent { get; private set; }
        
        public void UseWeaponRequest()
        {
            if (currentWeapon)
            {
                currentWeapon.Use();
            }
        }

        public void InstantiateWeapon(BaseWeapon weaponPrefab)
        {
            if(currentWeapon != null) Destroy(currentWeapon.gameObject);
            
            currentWeapon = Instantiate(weaponPrefab, transform);
            currentWeapon.InitializeWeapon(this);
        }
    }
}
