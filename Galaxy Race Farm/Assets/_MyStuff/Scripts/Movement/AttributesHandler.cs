using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttributesHandler : MonoBehaviour
{
    [SerializeField] StateHandler stateHandler;

    void Start()
    {
        stateHandler = GetComponent<StateHandler>();
    }

    void Update()
    {
        
    }
    public void AddAttributes()
    {

    }
    public void AddFarmSpeedAttributes()
    {

    }
    public void AddFarmCombinedAttributes()
    {
        //if (stateHandler.currentState == stateHandler.State.Racing)
        {
            //speed += bonusSpeed + permBonusSpeed;
            //acceleration += bonusAcceleration + permBonusAcceleration;
            //maxSpeed += bonusMaxSpeed + permBonusMaxSpeed;
        }
    }
}
