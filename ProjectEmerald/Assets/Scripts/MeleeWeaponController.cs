using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour
{
    public Transform weaponHold;
    public MeleeWeapon startingMelee;
  
    public MeleeWeapon equippedMelee;

    GunController gunController;

    void Start()
    {

        gunController = GetComponent<GunController>();

        if (startingMelee != null)
        {
            
            EquipMelee(startingMelee);

            if (gunController.equippedGun != null)
            {
                Destroy(gunController.equippedGun.gameObject);
            }
        }
    }

    public void EquipMelee(MeleeWeapon meleeToEquip)
    {
        if (equippedMelee != null)
        {
            Destroy(equippedMelee.gameObject);
            if (gunController.equippedGun != null)
            {
                Destroy(gunController.equippedGun.gameObject);
            }
        }
        equippedMelee = Instantiate(meleeToEquip, weaponHold.position, weaponHold.rotation) as MeleeWeapon;
        equippedMelee.transform.parent = weaponHold;
    }

    public void Swing()
    {
        if (equippedMelee != null)
        {
            equippedMelee.Swing();
        }
    }
}
