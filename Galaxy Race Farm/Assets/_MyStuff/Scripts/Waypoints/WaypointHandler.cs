using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointHandler : MonoBehaviour
{
    [SerializeField] private List<string> farmingRoute = new List<string>();
    [SerializeField] int currentWaypointNumber;
    [SerializeField] List<GameObject> waypoints;
    [SerializeField] AttributesHandler attributesHandler;
    [SerializeField] StateHandler stateHandler;

    void Start()
    {
        stateHandler = GetComponent<StateHandler>();
        attributesHandler = GetComponent<AttributesHandler>();
        currentWaypointNumber = 0;
    }

    void Update()
    {
        
    }

    public void WaypointHandling(Collider other)
    {
        WaypointInfo WaypointInfo = other.GetComponent<WaypointInfo>();
        GameObject grandparentGameObject = other.transform.parent?.parent.gameObject;
        GameObject parentGameObject = other.transform.parent?.gameObject;

        WaypointInfo GPWaypointInfo = grandparentGameObject.GetComponent<WaypointInfo>();

        if (waypoints == null || waypoints.Count == 0 || waypoints[currentWaypointNumber] == null) return;
        if (other.transform.position != waypoints[currentWaypointNumber].gameObject.transform.position) return;
        if (GPWaypointInfo != null && other.GetComponent<LastWP>() != null)
        {
            attributesHandler.AddAttributes();
        }
        if (GPWaypointInfo != null && GPWaypointInfo.type == "FarmSpeed" && other.GetComponent<LastWP>() != null)
        {
            attributesHandler.AddFarmSpeedAttributes();
        }
        if (GPWaypointInfo != null && GPWaypointInfo.type == "#FarmSpeed" && other.GetComponent<LastWP>() != null)
        {
            attributesHandler.AddFarmSpeedAttributes();
        }
        if (WaypointInfo != null && WaypointInfo.type == "start")
        {
            stateHandler.ChangeState(StateHandler.State.Racing);
            attributesHandler.AddFarmCombinedAttributes();
        }
        if (WaypointInfo != null && WaypointInfo.type == "FinishLine")
        {
            RaceManager.Instance.RegisterFinished(this.gameObject);
        }
        currentWaypointNumber++;
    }
}
