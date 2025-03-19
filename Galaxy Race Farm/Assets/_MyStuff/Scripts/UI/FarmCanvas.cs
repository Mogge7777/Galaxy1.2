using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmCanvas : MonoBehaviour, IResetable
{
    [SerializeField] List<GameObject> farmPlanetList = new List<GameObject>();
    [SerializeField] List<GameObject> planetButtons = new List<GameObject>();

    [SerializeField] List<GameObject> myCurrentPlanetsON = new List<GameObject>();

    [SerializeField] GameObject doneButton;

    void Start()
    {
        StartCoroutine(UIRightButtonsONEnumerator());
    }

    void Update()
    {

    }

    //void initFarmPlanetList()
    //{
    //    if (farmPlanetList.Count > 0)
    //    {
    //        farmPlanetList = RaceManager.Instance.planetList;
    //    }
    //    else 
    //    {
    //        if (Time.time < 5)
    //        {
    //            initFarmPlanetList();
    //        }
    //        else
    //        {
    //            Debug.Log("no planet list");
    //        }
    //    }
    //}
    
    IEnumerator UIRightButtonsONEnumerator()
    {
        foreach (GameObject go in planetButtons) 
        { 
            go.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        
        myCurrentPlanetsON = RaceManager.Instance.currentPlanetsON;
        
        foreach (GameObject currentPlanet in myCurrentPlanetsON)
        {
            foreach (GameObject button in planetButtons)
            {
                if (currentPlanet.name == button.name)
                {
                    button.SetActive(true);
                }
            }
        }
        doneButton.gameObject.SetActive(true);
    }
    public void Reset()
    {
        StartCoroutine(UIRightButtonsONEnumerator());
    }
}
