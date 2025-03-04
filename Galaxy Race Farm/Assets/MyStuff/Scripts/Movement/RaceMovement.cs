using System;
using System.Collections.Generic;
using UnityEngine;

public class RaceMovement : MonoBehaviour, IResetable
{
    [SerializeField] private List<string> farmingRoute = new List<string>();

    [SerializeField] int currentWaypointNumber;
    [SerializeField] float speed = 40f, acceleration = 5f, maxSpeed = 100;
    [SerializeField] float farmSpeed = 15, farmMaxSpeed = 200, farmAcceleration = 0;
    [SerializeField] float bonusSpeed = 0f, bonusAcceleration = 0f, bonusMaxSpeed = 0;
    [SerializeField] float rotateSpeed = 10f;
    public float permBonusSpeed, permBonusAcceleration, permBonusMaxSpeed, permFarmSpeed;   

    [SerializeField] List<GameObject> waypoints;

    [SerializeField] enum State { PreRace, Farming, Racing, Finished } 
    [SerializeField] State currentState;

    [SerializeField] private event Action<State> OnStateChanged;

    private Vector3 startPos, initialStartPos;
    public float startSpeed, startAcceleration, startMaxSpeed, startFarmSpeed, StartFarmMaxSpeed, startFarmAcceleration,
                  startBonusSpeed, startBonusAcceleration, StartBonusMaxSpeed, startRotation, startPermBonusSpeed,
                  startPermBonusAcceleration, startPermBonusMaxSpeed, startPermFarmSpeed; 
    void Start()
    {
        currentWaypointNumber = 0;
        ChangeState(State.PreRace);

        RaceManager.Instance.registerRacer(this.gameObject, 0);

        startPos = transform.position;
        GetRacerStartAttributes();
    }

    void FixedUpdate()
    {
        RaceMove();
        distanceToFinish();
    }

    void RaceMove()
    {
        if (currentState != State.Farming  && currentState != State.Racing) return;
        if (speed > maxSpeed) speed = maxSpeed;
        if (farmSpeed > farmMaxSpeed) farmSpeed = farmMaxSpeed;
        if (waypoints.Count >= 1 && currentWaypointNumber < waypoints.Count) 
        {
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

    //Player button choice
    public void GetWaypooints(string WPType)
    {
        List<GameObject> tempWaypoints = WaypointManager.Instance.GetPlanetWaypoints(WPType);
        farmingRoute.Add(WPType);
        foreach (GameObject waypoint in tempWaypoints)
        {
            waypoints.Add(waypoint);
        }
        tempWaypoints.Clear();
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
        if (GPWaypointInfo != null && GPWaypointInfo.type == "FarmSpeed" && other.GetComponent<LastWP>() != null)
        {
            addFarmSpeed(other);
            Debug.Log("addfarm1");
        }
        if (GPWaypointInfo != null && GPWaypointInfo.type == "#FarmSpeed" && other.GetComponent<LastWP>() != null)
        {
            addPermFarmSpeed(other);
            Debug.Log("addfarm2");
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
        Transform grandparentTransform = other.transform.parent?.parent;
        WaypointInfo GPWaypointInfo = grandparentTransform.GetComponent<WaypointInfo>();
        
        farmSpeed += GPWaypointInfo.extraFarmSpeed;
    }
    public void addPermFarmSpeed(Collider other)
    {
        Transform grandparentTransform = other.transform.parent?.parent;
        WaypointInfo GPWaypointInfo = grandparentTransform.GetComponent<WaypointInfo>();

        permFarmSpeed += GPWaypointInfo.permFarmSpeed;
        farmSpeed += GPWaypointInfo.permFarmSpeed;
    }
    private void ChangeState(State newState)
    {
        if (currentState != newState)
        {
            currentState = newState;

            OnStateChanged?.Invoke(currentState);
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
        startPermFarmSpeed = 0;


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
            if (distanceToFinish <= 10f) distanceToFinish = 0;
            if (distanceToFinish == 0 && endResultGiven == false)
            {
                DistanceToFinishManager.Instance.RegisterDistanceToFinish(0, distanceToFinish, (int)speed);
                endResultGiven = true;
            }
            if (distanceToFinish > 10f) DistanceToFinishManager.Instance.RegisterDistanceToFinish(0, distanceToFinish, (int)speed);
        }
    }
}
