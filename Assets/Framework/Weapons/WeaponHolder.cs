using Framework.Stats;
using UnityEngine;

namespace Framework.Weapons
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private BaseWeapon currentWeapon;
        [SerializeField] private StatsComponent statsComponent;
        
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
        }
    }
}
