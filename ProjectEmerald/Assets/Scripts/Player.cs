using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
[RequireComponent(typeof(ControlPanelController))]
public class Player : MonoBehaviour
{

    public float moveSpeed = 5;

    Camera viewCamera;
    PlayerController controller;
    GunController gunController;
    ControlPanelController controlPanel;

    void Start()
    {
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        controlPanel = GetComponent<ControlPanelController>();
        viewCamera = Camera.main;
    }

    
    void Update()
    {

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Vertical"), 0, -(Input.GetAxisRaw("Horizontal")));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

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

        //Change Gun Input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunController.EquipGun(gunController.startingGun);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunController.EquipGun(gunController.pistol);
        }

        //Interact with control panels
        if (Input.GetKeyDown(KeyCode.E))
        {
            controlPanel.activated = true;
        }

    }


}
