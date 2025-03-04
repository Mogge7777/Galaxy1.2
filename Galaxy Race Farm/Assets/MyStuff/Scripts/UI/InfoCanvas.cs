using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI StartSpeed, Acceleration, MaxSpeed, FarmSpeed, permStartSpeed, permAcceleration, permMaxSpeed, permFarmSpeed;
    [SerializeField] string StartSpeedString, AccelerationString, MaxSpeedString, FarmSpeedString, 
                    permStartSpeedString, permAccelerationString, permMaxSpeedString, permFarmSpeedString;
    [SerializeField] RaceMovement player1;

    private void Start()
    {
        StartCoroutine(UpdateUIIenumerator());
    }

    void Update()
    {
        
    }
    public void UpdateUI()
    {
        StartSpeedString = null;
        AccelerationString = null;
        MaxSpeedString = null;
        FarmSpeedString = null;
        permStartSpeedString = null;
        permAccelerationString = null;
        permMaxSpeedString = null;
        permFarmSpeedString = null;

        StartSpeedString += player1.startSpeed.ToString();
        AccelerationString += player1.startAcceleration.ToString();
        MaxSpeedString += player1.startMaxSpeed.ToString();
        FarmSpeedString += player1.startFarmSpeed.ToString();
        permStartSpeedString += player1.permBonusSpeed.ToString();
        permAccelerationString += player1.permBonusAcceleration.ToString();
        permMaxSpeedString += player1.permBonusMaxSpeed.ToString();
        permFarmSpeedString += player1.permFarmSpeed.ToString();

        StartSpeed.text = "StartSpeed: " + StartSpeedString;
        Acceleration.text = "Acceleratio: " +AccelerationString;
        MaxSpeed.text = "MAxSpeed: " +MaxSpeedString;
        FarmSpeed.text = "FarmSpeed: " + FarmSpeedString;
        permStartSpeed.text = "permStartSpeed: " + permStartSpeedString;
        permAcceleration.text = "permAcceleration: " + permAccelerationString;
        permMaxSpeed.text = "permMaxSpeed: " + permMaxSpeedString;
        permFarmSpeed.text = "permFarmSpeed: " + permFarmSpeedString;
    }
    public IEnumerator UpdateUIIenumerator()
    {
        yield return new WaitForSeconds(0.6f);
        UpdateUI();
    }
}
