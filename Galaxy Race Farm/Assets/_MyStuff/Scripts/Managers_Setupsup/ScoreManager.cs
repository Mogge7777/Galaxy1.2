using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    public string RacerName;
    public float totalScore;
}


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = new ScoreManager();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void RegisterScore(List<GameObject> racers)
    {
        foreach (GameObject racer in racers)
        {

        }
    }
}
