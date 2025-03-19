using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterPlanet : MonoBehaviour
{
    void Start()
    {
        RaceManager.Instance.RegisterPlanet(this.gameObject); 
        this.gameObject.SetActive(false);
    }
}
