using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    [SerializeField] public enum State { PreRace, Farming, Racing, Finished }
    [SerializeField] public State currentState;
    [SerializeField] public event Action<State> OnStateChanged;
    void Start()
    {
        
    }


    void Update()
    {
        
    }
    public void ChangeState(State newState)
    {
        if (currentState != newState)
        {
            currentState = newState;

            OnStateChanged?.Invoke(currentState);
        }
    }
}
