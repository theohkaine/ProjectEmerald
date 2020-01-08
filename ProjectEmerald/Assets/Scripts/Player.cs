using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
//[RequireComponent(typeof(MeleeWeaponController))]
public class Player : LivingEntity
{

    public float moveSpeed = 5;
    

    Camera viewCamera;
    PlayerController controller;
    GunController gunController;
    //MeleeWeaponController meleeController;
   

    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
       
        //meleeController = GetComponent<MeleeWeaponController>();

        viewCamera = Camera.main;
       
    }

    
    void Update()
    {

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Vertical"), 0, -(Input.GetAxisRaw("Horizontal")));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;

        controller.MovementManager(moveVelocity);
        
        
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            controller.LookAt(point);

            Debug.DrawLine(ray.origin, point, Color.red);   //FOR DEBUGGING PURPOSES
        }

        //Weapon Input
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }

        /*
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.MovementManager(moveVelocity);
            controller.currentState = PlayerController.State.sneaking;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            controller.MovementManager(moveVelocity);
            controller.currentState = PlayerController.State.running;
        }
        */

        //Change Gun Input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //meleeController.EquipMelee(meleeController.startingMelee);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunController.EquipGun(gunController.flashlight);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gunController.EquipGun(gunController.pistol);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            gunController.EquipGun(gunController.shotgun);
        }

    }


}
