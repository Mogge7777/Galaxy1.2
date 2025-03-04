using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    RaceMovement raceMovement;
    void Start()
    {
        raceMovement = GetComponent<RaceMovement>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
