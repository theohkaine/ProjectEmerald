using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum State { locked, open, close };
    public State currentState;

    Transform target;
    LivingEntity targetEntity;

    static Animator anim;
    bool hasTarget;

    public float sqrDistanceToTarget;

    protected void Start()
    {
        anim = GetComponent<Animator>();
        
        anim.SetInteger("doorPosition", 2);

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            currentState = State.close;
        
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;
        }
    }

    public void FixedUpdate()
    {
        sqrDistanceToTarget = (target.position - transform.position).sqrMagnitude;
        print(sqrDistanceToTarget);

        if (sqrDistanceToTarget < 30)
        {
            openDoor();
        }
        else
        {
            closeDoor();
        }
    }

    void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.locked;
        lockedDoor();
    }

    //ANIMATIONS
    //======================================================
    void lockedDoor()
    {
        anim.SetInteger("doorPosition", 0);
        currentState = State.locked;
    }

    void openDoor()
    {
        anim.SetInteger("doorPosition", 1);
        currentState = State.open;
    }

    void closeDoor()
    {
        anim.SetInteger("doorPosition", 2);
        currentState = State.close;
    }


    //======================================================
}
