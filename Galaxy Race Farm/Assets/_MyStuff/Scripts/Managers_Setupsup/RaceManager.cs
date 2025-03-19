using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{

    [SerializeField] CompetitionManager competitionManager;
    [SerializeField] CompetitionFlow competitionFLow;

    //[SerializeField] List<GameObject> racers = new List<GameObject>();

    [SerializeField] List<GameObject> finishingOrderList = new List<GameObject>();
    [SerializeField] List<float> finishingOrderTimes = new List<float>();

    [SerializeField] int amountOfRacers, amountOfRaces, currentRaceNr, amountOfPlanets;

    [SerializeField] float raceTime;
    [SerializeField] List<int> finishingTtimes = new List<int>();
    [SerializeField] List<IRaceType> raceSchedule = new List<IRaceType>();

    public List<GameObject> planetList = new List<GameObject>();

    private List<IResetable> resetables;

    public List<GameObject> racersRegistredList;

    public List<int> pointsList;

    public static RaceManager Instance { get; private set; }

    [SerializeField] GameObject farmUI, InfOUI;

    //public static event Action OnNewRaceEvent;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
        amountOfPlanets = competitionManager.amountOfPlanets;
        currentRaceNr = 1;
        raceSchedule.Add(new RT_Regular());
        amountOfRacers = competitionManager.amountOfPlayers;
        amountOfRaces = competitionManager.amountOfRaces;

        pointsList = competitionManager.scoringList;

        farmUI = GameObject.Find("FarmCanvas");
        InfOUI = GameObject.Find("Info Canvas");

        StartCoroutine(LateInitialicer());
    }
    IEnumerator LateInitialicer()
    {
        yield return new WaitForSeconds(0.1f);
        TurnOnRightPlanets();
    }
    
    public void Inizialize(CompetitionManager setCompetitionManager, CompetitionFlow setCompetitionFlow)
    {
        competitionManager = setCompetitionManager;
        competitionFLow = setCompetitionFlow;
    }

    void Update()
    {
        StartRaceTime();
    }

    //TODO: Add so time only starts when all racers ready to race
    public void StartRaceTime()
    {
        raceTime += Time.deltaTime;
    }

    //------done by racer to register for race------
    public void registerRacer(GameObject racer, int id)
    {
        if (racer == null) return;
        racersRegistredList.Add(racer);
    }
    
    //------done by racer to register it has finished------
    public void RegisterFinished(GameObject racer)
    {
        if (racersRegistredList.Contains(racer))
        {
            if (finishingOrderList.Count == amountOfRacers)
            {
                return;
            }
            finishingOrderList.Add(racer);
            float roundedTime = (float)Math.Round(raceTime, 2);
            finishingOrderTimes.Add(roundedTime);
            
            if (finishingOrderList.Count == amountOfRacers)
            {
                SendResults(finishingOrderList, finishingOrderTimes);
            }
        }
    }

    //-----done by racemanager when registred results for all racers-----
    public void SendResults(List<GameObject> racersFinishingList, List<float> racersFinishingTimes)
    {   
        if (racersFinishingTimes.Count == racersRegistredList.Count)
        {
            SceneManager.LoadScene("PostRaceUI", LoadSceneMode.Additive);
            StartCoroutine(WaitForManagerAndShowResults(racersFinishingList, racersFinishingTimes));
        }
    }
    public IEnumerator WaitForManagerAndShowResults(List <GameObject> ListGameObjects, List<float> ListFloats )
    {
        while (UIManager.Instance == null)
        {
            yield return null;
        }
        UIManager.Instance.ShowResults(ListGameObjects, ListFloats);
    }
   
    public void NewRace()
    {
        
        if (currentRaceNr == amountOfRaces)
        {
            Debug.Log("COMPETITION FINISHED");
            return;
        }
        
        resetables = new List<IResetable>(FindObjectsOfType<MonoBehaviour>().OfType<IResetable>());

        Reset();

        foreach (var resetable in resetables)
        {
            resetable.Reset();
        }

        SceneManager.UnloadSceneAsync("PostRaceUI");
        farmUI.gameObject.SetActive(true);
        InfOUI.gameObject.SetActive(true);

        UIExeption();

        currentRaceNr++;
    }
    public void Reset()
    {
        finishingOrderList.Clear();
        finishingOrderTimes.Clear();

        pointsList = competitionManager.scoringList;

        TurnOnRightPlanets();

        raceTime = 0;
    }

    public void RegisterPlanet(GameObject planet)
    {
        planetList.Add(planet);
    }

    public List<GameObject> currentPlanetsON = new List<GameObject>();
    void TurnOnRightPlanets()
    {
        currentPlanetsON.Clear();

        foreach (var planet in planetList) { 
            planet.gameObject.SetActive(false);
        }
        //seeding the random generator on mousepsition
        int seedNR = ((int)Input.mousePosition.x * 39583) + ((int)Input.mousePosition.y * 39583) - ((int)Input.mousePosition.z * 39583); 
        UnityEngine.Random.InitState(seedNR);

        if (amountOfPlanets < 1)
        {
            amountOfPlanets = 4;
            Debug.Log("amountOfPlanets not defined but now set to 4");
        }

        List<int> allIndexes = new List<int>();
        for (int i = 0; i < planetList.Count; i++)
        {
            allIndexes.Add(i);
        }

        for (int i = 0; i < amountOfPlanets; i++)
        {
            int randomPlanetIndex = UnityEngine.Random.Range(0, allIndexes.Count);

            int planetIndex = allIndexes[randomPlanetIndex];

            planetList[planetIndex].SetActive(true);

            currentPlanetsON.Add(planetList[planetIndex]);

            allIndexes.RemoveAt(randomPlanetIndex);
        }
    }
    void UIExeption()
    {
        farmUI.gameObject.SetActive(true);
        InfOUI.gameObject.SetActive(true);
        
        InfOUI.GetComponent<InfoCanvas>().UpdateUI();
        FarmCanvas farmCanvas;
        farmCanvas = farmUI.GetComponent<FarmCanvas>();
        farmCanvas.Reset();
    }
}
