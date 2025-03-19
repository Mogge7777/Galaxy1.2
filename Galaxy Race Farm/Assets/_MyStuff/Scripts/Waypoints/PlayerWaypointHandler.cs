using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaypointHandler : MonoBehaviour
{
    [SerializeField] private List<string> farmingRoute = new List<string>();

    [SerializeField] List<GameObject> waypoints;
    void Start()
    {

    }

    void Update()
    {
        
    }
    public void GetWaypointsFromWPManager(string WPType)
    {
        List<GameObject> tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(WPType);
        farmingRoute.Add(WPType);
        foreach (GameObject waypoint in tempWaypoints)
        {
            waypoints.Add(waypoint);
        }
        tempWaypoints.Clear();
    }
    public List<GameObject> GetWaypointsFromPlayerWPHandler()
    {
        //out theFarmingRoute = farmingRoute;
        return waypoints;
    }
}
