using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 10;

    float lifeTime = 3;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }
}
