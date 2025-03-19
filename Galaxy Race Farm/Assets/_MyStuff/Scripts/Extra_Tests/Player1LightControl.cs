using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1LightControl : MonoBehaviour
{
    Light light;
    [SerializeField] float lightPulseSpeed;
    [SerializeField] float lightPulseStrenght;

    void Start()
    {
        light = gameObject.GetComponentInChildren<Light>();
        if (light.intensity < 1f)
        {
            lightPulseSpeed = 5f;
            lightPulseStrenght = 20f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        light.intensity = Mathf.Sin(Time.time * lightPulseSpeed) * lightPulseStrenght;

        //if (light.intensity >= 0 && light.intensity < 20 )
        //{
            
        //}
        //else if (light.intensity >= 20)
        //{
        //    light.intensity -= 10 * Time.deltaTime;
        //}
    }
}
