using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRaceMovement : MonoBehaviour, IResetable
{
    [SerializeField] private List<string> farmingRoute = new List<string>();

    [SerializeField] int currentWaypointNumber, minDiceThrow, maxDiceThrow, minFarmDiceThrow, maxFarmDiceThrow;
    [SerializeField] float speed = 40f, acceleration = 5f, maxSpeed = 100;  
    [SerializeField] float farmSpeed = 15, farmMaxSpeed = 200f, farmAcceleration = 0;
    [SerializeField] float bonusSpeed = 0f, bonusAcceleration = 0f, bonusMaxSpeed = 0;
    [SerializeField] float rotateSpeed = 150f;
    [SerializeField] float permBonusSpeed, permBonusAcceleration, permBonusMaxSpeed;

    [SerializeField] List<GameObject> waypoints;

    [SerializeField] enum State { PreRace, Farming, Racing, Finished } 
    [SerializeField] State currentState;

    [SerializeField] private event Action<State> OnStateChanged;

    public int playerID = 0, wait;

    [SerializeField] List<GameObject> tempWaypoints;

    private Vector3 startPos, initialStartPos;
    private float startSpeed, startAcceleration, startMaxSpeed, startFarmSpeed, StartFarmMaxSpeed, startFarmAcceleration,
                  startBonusSpeed, startBonusAcceleration, StartBonusMaxSpeed, startRotation, startPermBonusSpeed,
                  startPermBonusAcceleration, startPermBonusMaxSpeed;


    void Start()
    {
        //just to not get console yellow warning
        farmMaxSpeed++;

        currentWaypointNumber = 0;
        ChangeState(State.PreRace);
        
        StartCoroutine("WaitBeforeFarming"); //TODO: startEvent
 
        RaceManager.Instance.registerRacer(this.gameObject, this.playerID);

        startPos = transform.position;
        GetRacerStartAttributes();
    }
    IEnumerator WaitBeforeFarming()
    {
        DistanceToFinishManager.Instance.Countdown(wait);
        yield return new WaitForSeconds(wait);
        GetWaypooints();
        ChangeState(State.Farming);
        RaceManager.Instance.StartRaceTime();
    }
    void FixedUpdate()
    {
        RaceMove();
        distanceToFinish();
    }

    void RaceMove()
    {
        if (currentState != State.Farming && currentState != State.Racing) return;
        if (speed > maxSpeed) speed = maxSpeed;
        if (waypoints.Count >= 1 && currentWaypointNumber < waypoints.Count)
        {
            //counts acceleration in farm or race to add to speed pre movetowards//
            if (currentState == State.Farming) speed = Mathf.Min(farmSpeed + farmAcceleration * Time.deltaTime, maxSpeed);
            else if (currentState == State.Racing) speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);

            transform.position = Vector3.MoveTowards
                (transform.position, waypoints[currentWaypointNumber].gameObject.transform.position, speed * Time.deltaTime);


            //prework for transform.rotation//
            Vector3 targetDirection = waypoints[currentWaypointNumber].gameObject.transform.position - transform.position;
            
            Quaternion targetRotation = new Quaternion();
            if (targetDirection != Vector3.zero)
            {
                targetRotation = Quaternion.LookRotation(targetDirection);
            }

            transform.rotation = Quaternion.RotateTowards
                (transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        OnWaypointHit(other);
    }

    public void OnWaypointHit(Collider other)
    {
        WaypointInfo WaypointInfo = other.GetComponent<WaypointInfo>();

        GameObject grandparentGameObject = other.transform.parent?.parent.gameObject;
        GameObject parentGameObject = other.transform.parent?.gameObject;

        WaypointInfo GPWaypointInfo = grandparentGameObject.GetComponent<WaypointInfo>();

        if (waypoints == null || waypoints.Count == 0 || waypoints[currentWaypointNumber] == null) return;
        if (other.transform.position != waypoints[currentWaypointNumber].gameObject.transform.position) return;
        if (GPWaypointInfo != null && other.GetComponent<LastWP>() != null)
        {
            bonusSpeed += GPWaypointInfo.extraMoveSpeed;
            bonusAcceleration += GPWaypointInfo.extraAcceleration;
            bonusMaxSpeed += GPWaypointInfo.extraMaxSpeed;

            permBonusSpeed += GPWaypointInfo.permBonusSpeed;
            permBonusAcceleration += GPWaypointInfo.permBonusAcceleration;
            permBonusMaxSpeed += GPWaypointInfo.permBonusMaxSpeed;
        }
        if (GPWaypointInfo != null && GPWaypointInfo.type == "farmSpeed")
        {
            addFarmSpeed(other);
        }
        if (WaypointInfo != null && WaypointInfo.type == "start")
        {
            ChangeState(State.Racing);
            AddFarmCombinedAttributes();
        }
        if (WaypointInfo != null && WaypointInfo.type == "FinishLine")
        {
            RaceManager.Instance.RegisterFinished(this.gameObject);
        }
        currentWaypointNumber++;
    }
    public void addFarmSpeed(Collider other)
    {
        //WaypointInfo waypointInfo = other.gameObject.GetComponent<WaypointInfo>();

        Transform grandparentTransform = other.transform.parent?.parent;
        WaypointInfo GPWaypointInfo = grandparentTransform.GetComponent<WaypointInfo>();

        farmSpeed += GPWaypointInfo.extraFarmSpeed;
    }
    private void ChangeState(State newState)
    {
        if (currentState != newState)
        {
            currentState = newState;

            OnStateChanged?.Invoke(currentState);
        }
    }
    public void GetWaypooints()
    {
        int dicethrow = UnityEngine.Random.Range(minDiceThrow, maxDiceThrow);
        int farmSPeedDiceThrow = UnityEngine.Random.Range(minFarmDiceThrow, maxFarmDiceThrow);

        List<GameObject> tempWaypoints = new List<GameObject>();
        
        //Decide on farmSpeed first
        int caseForFarmSpeed = UnityEngine.Random.Range(0, 3); //0-2
        if (caseForFarmSpeed == 0) //farm if 1
        {
            
            for (int i = 0; i < farmSPeedDiceThrow; i++)
            {
                string choice = "FarmSpeed";
                tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(choice);
                farmingRoute.Add(choice);
                foreach (GameObject waypoint in tempWaypoints)
                {
                    waypoints.Add(waypoint);
                }
            }
        }

        //Decide other farm atttributes
        for (int i = 0; i < dicethrow; i++) //1-3
        {
            int j = UnityEngine.Random.Range(0, 10);

            if (j == 0)
            {
                string choice = "Startpeed";
                tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(choice);
                farmingRoute.Add(choice);
            }
            if (j == 1)
            {
                string choice = "MaxSpeed";
                tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(choice);
                farmingRoute.Add(choice);
            }
            if(j == 2)
            {
                string choice = "Acceleration";
                tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(choice);
                farmingRoute.Add(choice);
            }
            if (j == 3)
            {
                string choice = "#StartSpeed";
                tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(choice);
                farmingRoute.Add(choice);
            }
            if (j == 4)
            {
                string choice = "#Acceleration";
                tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(choice);
                farmingRoute.Add(choice);
            }
            if (j == 5)
            {
                string choice = "#FarmSpeed";
                tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(choice);
                farmingRoute.Add(choice);
            }
            if (j == 6)
            {
                string choice = "StartSpeed";
                tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(choice);
                farmingRoute.Add(choice);
            }
            if (j == 7)
            {
                string choice = "MaxSpeeed";
                tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(choice);
                farmingRoute.Add(choice);
            }
            if (j == 8)
            {
                string choice = "Acceleration";
                tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(choice);
                farmingRoute.Add(choice);
            }
            if (j == 9)
            {
                string choice = "MaxSpeed";
                tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(choice);
                farmingRoute.Add(choice);
            }

            foreach (GameObject waypoint in tempWaypoints)
            {
                waypoints.Add(waypoint);
            }
            tempWaypoints.Clear();
        }
        string raceTrackID;
        raceTrackID = "race" + playerID.ToString();

        if (WaypointManager.Instance.GetPlanetWaypoints(raceTrackID) != null)
        {
            farmingRoute.Add(raceTrackID);
            tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(raceTrackID);
        }
        else
        {
            Debug.LogWarning($"No waypoints found for raceTrackID: {raceTrackID}");
        }
        foreach (GameObject waypoint in tempWaypoints)
        {
            waypoints.Add(waypoint);
        }
    }
    
    private void AddFarmCombinedAttributes()
    {
        if (currentState == State.Racing)
        {
            speed += bonusSpeed + permBonusSpeed;
            acceleration += bonusAcceleration + permBonusAcceleration;
            maxSpeed += bonusMaxSpeed + permBonusMaxSpeed;
        }
    }
    
    bool FarmingStateSet;
    public void SetStateToFarming()
    {
        if (!FarmingStateSet)
        {
            ChangeState(State.Farming);
            FarmingStateSet = true;
        }
        else
        {
            Debug.LogWarning("Can´t set to farm twice!");
        }
    }
    public void DoneSelecting()
    {
        ChangeState(State.Farming);
    }
    private void GetRacerStartAttributes()
    {
        initialStartPos = startPos;

        startSpeed = speed;
        startAcceleration = acceleration;
        startMaxSpeed = maxSpeed;

        startFarmSpeed = farmSpeed;
        StartFarmMaxSpeed = farmMaxSpeed;
        startFarmAcceleration = farmAcceleration;

        startBonusSpeed = bonusSpeed;
        startBonusAcceleration = bonusAcceleration;
        StartBonusMaxSpeed = bonusMaxSpeed;

        startRotation = rotateSpeed;

                startPermBonusSpeed = 0;
        startPermBonusAcceleration = 0;
        startPermBonusMaxSpeed = 0;
    }
    public void Reset()
    {
        startPos = initialStartPos;
        transform.position = startPos;

        speed = startSpeed;
        acceleration = startAcceleration;
        maxSpeed = startMaxSpeed;

        farmSpeed = startFarmSpeed;
        farmMaxSpeed = StartFarmMaxSpeed;
        farmAcceleration = startFarmAcceleration;

        waypoints.Clear();
        farmingRoute.Clear();

        ChangeState(State.PreRace);

        currentWaypointNumber = 0;

        StartCoroutine(WaitBeforeFarming());

        RaceManager.Instance.StartRaceTime();

        bonusSpeed = startBonusSpeed;
        bonusAcceleration = startBonusAcceleration;
        bonusMaxSpeed = StartBonusMaxSpeed;
        rotateSpeed = startRotation;
    }
    bool endResultGiven = false;
    float previousDistance;
    void distanceToFinish()
    {
        if (currentState == State.Racing)
        {
            int lastIndex = waypoints.Count - 1;
            Vector3 currentPosition = transform.position;
            float distanceToFinish = Vector3.Distance(currentPosition, waypoints[lastIndex].gameObject.transform.position);
            if (distanceToFinish <= 10) distanceToFinish = 0;
            if (distanceToFinish == 0 && endResultGiven == false)
            {
                DistanceToFinishManager.Instance.RegisterDistanceToFinish(playerID, distanceToFinish, (int)speed);
                endResultGiven = true;
            }
            if (distanceToFinish > 10f) DistanceToFinishManager.Instance.RegisterDistanceToFinish(playerID, distanceToFinish, (int)speed);
        }
    }

}
