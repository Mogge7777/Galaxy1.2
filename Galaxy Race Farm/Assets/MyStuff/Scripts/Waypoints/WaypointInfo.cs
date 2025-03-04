using UnityEngine;

public class WaypointInfo : MonoBehaviour
{
    //raw attributes
    public float extraMoveSpeed = 0;
    public float extraAcceleration = 0;
    public float extraMaxSpeed = 0;

    //farming
    public float extraFarmSpeed = 0;

    //delay bonuses
    public float DelayedMoveSpeed = 0;
    public float timeDelay = 0;

    //permanent bonuses
    public float permBonusSpeed;
    public float permBonusAcceleration;
    public float permBonusMaxSpeed;
    public float permFarmSpeed;

    public string type;
}
