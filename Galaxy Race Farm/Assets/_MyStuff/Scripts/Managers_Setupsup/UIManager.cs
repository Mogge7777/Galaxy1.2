using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    List<GameObject> racers = new List<GameObject>();
    List<float> times = new List<float>();

    [SerializeField] GameObject placementPanel, placementPanel1, placementPanel2, placementPanel3, placementPrefab;

    RaceManager raceManager;

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
        //DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        racers = RaceManager.Instance.racersRegistredList;
        placementPanel = new GameObject();
    }

    void Update()
    {
        
    }
    [SerializeField] public List<GameObject> placementPrefabsList = new List<GameObject>();
    [SerializeField] public List<int> scores = new List<int>();
    public void ShowResults(List<GameObject> getRacers, List<float> getTimes )
    {
        for (int i = 0; i < racers.Count; i++)
        {
            switch (i)
            {
                case < 11:
                    placementPanel = placementPanel1;
                    break;

                case < 21:
                    placementPanel = placementPanel2;
                    break;

                case < 31:
                    placementPanel = placementPanel3;
                    break;

                default:
                    break;
            }
            racers = getRacers;
            times = getTimes;
            placementPrefab = Instantiate(placementPrefab);
            placementPrefabsList.Add(placementPrefab);
            placementPrefab.transform.SetParent(placementPanel.transform, false);
            placementPrefab.GetComponent<TextMeshProUGUI>().text =
                "    " + racers[i].name.ToString() + " " + times[i].ToString();
        }
        StartCoroutine("ShowPoints");
    }
    private IEnumerator ShowPoints()
    {
        yield return new WaitForSeconds(3);
        ShowPointsLogic();
    }

    //TODO: use with scoremanager instead
    private void ShowPointsLogic()
    {
        scores = RaceManager.Instance.pointsList;
        int i = 0;
        foreach (GameObject go in placementPrefabsList)
        {
            go.GetComponent<TextMeshProUGUI>().text = "    " +  scores[i].ToString();
            i++;
        }
        StartCoroutine(WaitThenNewRace());
    }
    private IEnumerator WaitThenNewRace()
    {
        yield return new WaitForSeconds(3);
        RaceManager.Instance.NewRace();
    }
}
