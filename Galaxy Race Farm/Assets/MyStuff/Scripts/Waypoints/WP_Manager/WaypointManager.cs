using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetWaypointsData
{
    public string planetName;
    public List<GameObject> waypointList = new List<GameObject>();
}

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }
    
    //new
    public List<PlanetWaypointsData> PlanetWaypointsList = new List<PlanetWaypointsData>();

    //old
    private Dictionary<string, List<GameObject>> planetWaypoints = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //old add get waypoints
    #region
    public void AddWaypoints(string planetName, List<GameObject> waypoints)
    {
        if (!planetWaypoints.ContainsKey(planetName))
        {
            planetWaypoints[planetName] = new List<GameObject>();
        }
        planetWaypoints[planetName].AddRange(waypoints);
    }

    public List<GameObject> GetWaypoints(string planetName)
    {
        if (planetWaypoints.TryGetValue(planetName, out List<GameObject> waypoints))
        {
            List<GameObject> waypointsList = new List<GameObject>();
            foreach (GameObject waypoint in waypoints)
            {
                waypointsList.Add(waypoint);
            }
            return waypointsList;
        }
        return new List<GameObject>();
    }
    #endregion

    //new
    public void AddPlanetWaypoints(string planetName, List<GameObject> waypoints)
    {
        PlanetWaypointsList.Add(new PlanetWaypointsData { planetName = planetName, waypointList = waypoints });
    }
    public List<GameObject> GetPlanetWaypoints(string name)
    {
        var localPlanetwaypoints = PlanetWaypointsList.Find(p => p.planetName == name);

        if (localPlanetwaypoints != null)
        {
            return new List<GameObject>(localPlanetwaypoints.waypointList);
        }
        return new List<GameObject>();
    }
}
