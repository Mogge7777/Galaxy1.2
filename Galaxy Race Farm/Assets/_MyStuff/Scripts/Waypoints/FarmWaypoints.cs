using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmWaypoints : MonoBehaviour
{
    [SerializeField]
    List<GameObject> myWaypoints = new List<GameObject>();
    void Start()
    {
        AddWaypointsToSelf();
        AddWaypointsToManager();  
    }
    void AddWaypointsToSelf()
    {
        Transform firstChild = transform.GetChild(0);
        foreach (Transform child in firstChild)
        {
            myWaypoints.Add(child.gameObject);
        }
    }
    void AddWaypointsToManager()
    {
        WaypointInfo waypointInfo = GetComponent<WaypointInfo>();
        string type = waypointInfo.type;
        WaypointManager.Instance.AddPlanetWaypoints(type, myWaypoints);
        
        #region //old way adding wps prob obsolete now
        /*
        switch (type)
        {
            case "speed":
                WaypointManager.Instance.AddPlanetWaypoints("speed", myWaypoints);
                break;

            case "acc":
                WaypointManager.Instance.AddPlanetWaypoints("acc", myWaypoints);
                break;

            case "maxSpeed":
                WaypointManager.Instance.AddPlanetWaypoints("maxSpeed", myWaypoints);
                break;

            case "race":
                WaypointManager.Instance.AddPlanetWaypoints("race", myWaypoints);
                break;

            case "race1":
                WaypointManager.Instance.AddPlanetWaypoints("race1", myWaypoints);
                break;

            case "race2":
                WaypointManager.Instance.AddPlanetWaypoints("race2", myWaypoints);
                break;

            case "race3":
                WaypointManager.Instance.AddPlanetWaypoints("race3", myWaypoints);
                break;

            case "race4":
                WaypointManager.Instance.AddPlanetWaypoints("race4", myWaypoints);
                break;

            case "race5":
                WaypointManager.Instance.AddPlanetWaypoints("race5", myWaypoints);
                break;

            case "farmSpeed":
                WaypointManager.Instance.AddPlanetWaypoints("farmSpeed", myWaypoints);
                break;

            case "boost":
                WaypointManager.Instance.AddPlanetWaypoints("boost", myWaypoints);
                break;

            case "asteroidSpeed":
                WaypointManager.Instance.AddPlanetWaypoints("asteroidSpeed", myWaypoints);
                break;

            case "gasSpeed":
                WaypointManager.Instance.AddPlanetWaypoints("gasSpeed", myWaypoints);
                break;

            case "last50%Speed":
                WaypointManager.Instance.AddPlanetWaypoints("last50%Speed", myWaypoints);
                break;

            case "defendSpeed":
                WaypointManager.Instance.AddPlanetWaypoints("defendSpeed", myWaypoints);
                break;

            case "overtakeSpeed":
                WaypointManager.Instance.AddPlanetWaypoints("overtakeSpeed", myWaypoints);
                break;

            default:
                break;
        }
        */
        #endregion
    }
}
