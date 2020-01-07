using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Display : MonoBehaviour
{

    public TextMeshProUGUI health;
    

    void Start()
    {
        health.SetText("10");
    }

    public void SetHealth(string _health)
    {
        health.SetText(_health);
    }

}
