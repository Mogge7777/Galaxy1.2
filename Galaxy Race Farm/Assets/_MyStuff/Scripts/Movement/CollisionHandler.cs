using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    WaypointHandler waypointHandler;

    void Start()
    {
        waypointHandler = GetComponent<WaypointHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        waypointHandler.WaypointHandling(other);
    }
}
