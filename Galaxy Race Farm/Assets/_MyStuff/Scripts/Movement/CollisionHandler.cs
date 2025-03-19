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
        WaypointInfo WaypointInfo = other.GetComponent<WaypointInfo>();
        GameObject grandparentGameObject = other.transform.parent?.parent.gameObject;
        GameObject parentGameObject = other.transform.parent?.gameObject;
    }
}
