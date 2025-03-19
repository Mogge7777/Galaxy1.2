using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointHandler : MonoBehaviour
{
    [SerializeField] private List<string> farmingRoute = new List<string>();
    [SerializeField] int currentWaypointNumber;
    [SerializeField] List<GameObject> waypoints;

    void Start()
    {
        currentWaypointNumber = 0;
    }

    void Update()
    {
        
    }
}
