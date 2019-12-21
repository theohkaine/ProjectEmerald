using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CealingLights : MonoBehaviour
{

    public Light spotLight;

    bool lightOn;

    float startingLightIntensity = 3.0f;
    float offLightIntensity = 0.0f;
    float nextShineTime;
    public float msBetweenShine = 2000;


    void Start()
    {
        spotLight.intensity = startingLightIntensity;
        lightOn = true;

        ShiningLights(spotLight);
    }

    void Update()
    {
        ShiningLights(spotLight);
    }

    public void ShiningLights(Light spotLight)
    {
        if (Time.time > nextShineTime)
        {
            nextShineTime = Time.time + msBetweenShine / 1000;
            if (lightOn)
            {
                spotLight.intensity = offLightIntensity;
                lightOn = false;
            }
            else
            {
                spotLight.intensity = startingLightIntensity;
                lightOn = true;
            }
        }

    }

   
}
