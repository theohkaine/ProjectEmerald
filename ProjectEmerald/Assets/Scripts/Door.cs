using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum State { locked, open, close };
    State currentState;

    static Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentState = State.close;
        anim.SetInteger("doorPosition", 2);
    }

    public void Activate(Door door)
    {
        print("Reached 3");
        if (currentState != State.locked)
        {
            if (currentState == State.open)
            {
                anim.SetInteger("doorPosition", 2);
                currentState = State.close;
                print("CloseDoor");
            }
            if (currentState == State.close)
            {
                anim.SetInteger("doorPosition", 1);
                currentState = State.open;
                print("OpenDoor");
            }
        }
        else
        {

            //DOOR IS LOCKED
        }
    }
}
