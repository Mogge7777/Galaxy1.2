using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState currentState;
    public enum GameState
    {
        AllPreRace, AllRace, AllPostRace,
    }
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
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void ChangeState(GameState state)
    {
        if (currentState != state) 
        { 
            currentState = state;
        }
    }
}
