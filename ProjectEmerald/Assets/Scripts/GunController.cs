using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public Transform weaponHold;
    public Gun flashlight;
    public Gun pistol;
    public Gun equippedGun;

    MeleeWeaponController meleeController;

    void Start()
    {

        meleeController = GetComponent<MeleeWeaponController>();
        /*
        -MELEE IS NOW STARTING WEAPON
        if (startingGun != null)
        {
            EquipGun(startingGun);
        }
        */
    }

    public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
            if (meleeController.equippedMelee != null)
            {
                Destroy(meleeController.equippedMelee.gameObject);
            }
        }
        equippedGun = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Gun;
        equippedGun.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if (equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
