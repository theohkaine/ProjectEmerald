using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public enum State { idle, walking, sneaking, running };
    public State currentState;

    Vector3 velocity;
    Rigidbody rigidbody;

    static Animator anim;
    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void MovementManager(Vector3 _velocity)
    {
        velocity = _velocity;
        if (velocity.magnitude == 0)        //IDLE
        {
            Idle();
        }
        else if (currentState == State.sneaking)
        {
            Sneaking(velocity);
        }
        else if (currentState == State.running)
        {
            Running(velocity);
        }
        else
        {                                   //WALK
            Move(velocity);
        }
        
    }

    public void Idle()
    {
        currentState = State.idle;
        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isSneaking", false);
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
        currentState = State.walking;
        anim.SetBool("isWalking", true);
        anim.SetBool("isIdle", false);
        anim.SetBool("isSneaking", false);
    }

    public void Sneaking(Vector3 _velocity)
    {
        velocity = _velocity.normalized / 0.2f;
        currentState = State.sneaking;
        anim.SetBool("isSneaking", true);
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", false);
    }

    public void Running(Vector3 _velocity)
    {
        velocity = _velocity.normalized * 0.2f;
        currentState = State.running;
        anim.SetBool("isRunning", true);

        anim.SetBool("isSneaking", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", false);
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
    }
}
