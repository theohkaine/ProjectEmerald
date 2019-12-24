using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelController : MonoBehaviour
{
    Transform target;
    Door door;
    public Door targetDoor;

    float sqrDistanceToTarget;

    void Start()
    {
       target = GameObject.FindGameObjectWithTag("Player").transform;
        door = GetComponent<Door>();
    }

    public void FixedUpdate()
    {
        sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;
    }

    public void Activate()
    {
        if (sqrDistanceToTarget < 2)
        {
            door.Activate(targetDoor);
        }
        print("Reached 2");
    }
}
