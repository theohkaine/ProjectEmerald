using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    int value = 0;
    public void Activate(GameObject door)
    {
        door.GetComponent<Animator>().GetParameter(0);
    }
}
