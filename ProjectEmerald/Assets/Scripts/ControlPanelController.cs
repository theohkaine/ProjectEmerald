using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelController : MonoBehaviour
{
    Transform target;
    public ControlPanel panel;
    public bool activated = false;

    public GameObject door;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
       
    }

    void Update()
    {
        float sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;

        if (sqrDistanceToTarget < 2)
        {
            panel.Activate(door);
        }
    }
}
