using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitionManager : MonoBehaviour
{
    public int amountOfRaces, amountOfPlayers, amountOfPlanets;
    
    public bool passivesRoundsOn, noFarmRound;

    public float trackWidth;

    [SerializeField] TrackData trackDataInfo;

    [SerializeField] CompetitionFlow competitionFlow;

    [SerializeField] GameSettings competitionSettings;

    [SerializeField] RaceManager raceManager;

    [SerializeField] GameObject FarmUI, InfOUI;

    public List<int> scoringList;

    void Awake()
    {
        competitionFlow = GetComponent<CompetitionFlow>();
        competitionFlow.Initialize(this);

        raceManager = GetComponent<RaceManager>();
        raceManager.Inizialize(this, competitionFlow);

        amountOfPlayers = competitionSettings.players;
        amountOfRaces = competitionSettings.races;
        trackWidth = trackDataInfo.width;
        amountOfPlanets = competitionSettings.planets;

        FarmUI.gameObject.SetActive(true);
        InfOUI.gameObject.SetActive(true);

        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        for (int i = 0; i < amountOfPlayers; i++)
        {
            int score = amountOfPlayers + 1 - i;
            if (score == amountOfPlayers + 1) score += 2;
            scoringList.Add(score);
        }
    }

    void Update()
    {
        
    }
}
