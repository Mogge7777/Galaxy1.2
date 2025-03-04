using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompetitionFlow : MonoBehaviour
{
    [SerializeField]
    CompetitionManager competitionManager;

    [SerializeField]
    RaceMovement player1;

    [SerializeField]
    int amountOfRaces, amountOfPlayers, currentRaceNr;

    [SerializeField]
    float trackWidth;

    public GameObject track, parentToTracks, parentToRacers;

    public AIRaceMovement aIRacer;

    public List<IRaceType> raceOrder;

    void Start()
    {
        amountOfRaces = competitionManager.amountOfRaces;
        amountOfPlayers = competitionManager.amountOfPlayers;
        trackWidth = competitionManager.trackWidth;

        parentToTracks = new GameObject("Tracks_Parent");
        parentToRacers = new GameObject("------Racers_Parent------");

        CreatePlayers();
        MakeRaceOrder();
    }

    public void Initialize(CompetitionManager setCompetitionManager)
    {
        competitionManager = setCompetitionManager;
    }
    private void MakeRaceOrder()
    {
        //TODO: condition on player input
        if (true)
        {
            raceOrder = new List<IRaceType>();
            for (int i = 0; i < amountOfRaces; i++)
            {
                raceOrder.Add(new RT_Regular());
            }
        }
    }

    //since single player always only one player of the total
    //TODO: no parameters and not amountofplayersinfo since we already know that from start()
    private void CreatePlayers()
    {
        //Fix later: might not be vector3.zero in future
        
        Vector3 startPos = Vector3.zero;
        //instantiate player and their tracks
        for (int i = 0; i < amountOfPlayers; i++) 
        {   
            if (i == 0)
            {
                track = Instantiate(track);
                track.transform.position = startPos;

                track.transform.parent = parentToTracks.transform;

                WaypointInfo waypointInfo = track.GetComponent<WaypointInfo>();
                waypointInfo.type = "race" + i.ToString();

                //TODO: improve in many ways
                //player1 = Instantiate(player1);
                player1.transform.position = startPos;
                player1.transform.parent = parentToRacers.transform; 
            }
            else if (i > 0)
            {
                track = Instantiate(track);
                Vector3 currentPos = startPos + new Vector3(trackWidth * i, 0f, 0f);
                track.transform.position = currentPos;
                track.transform.parent = parentToTracks.transform;

                WaypointInfo waypointInfo = track.GetComponent<WaypointInfo>();
                waypointInfo.type = "race" + i.ToString();

                aIRacer = Instantiate(aIRacer);
                aIRacer.transform.position = currentPos;
                aIRacer.transform.parent = parentToRacers.transform;
                aIRacer.gameObject.name = "AIRacer" + i;
                
                aIRacer.playerID = i;
            }
        }
    }
    public void RaceFinished()
    {
        SceneManager.LoadScene("PostRaceUI", LoadSceneMode.Additive);
    }
    public void NextRace()
    {
        if (amountOfRaces <= currentRaceNr)
        {
            //end competition scene  
        }
        else
        {
            //nextRace
        }
    }
}
