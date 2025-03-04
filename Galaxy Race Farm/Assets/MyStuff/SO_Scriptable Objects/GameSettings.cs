using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGame/Settings", fileName = "CompetitionSettings")]
public class GameSettings : ScriptableObject
{
    public int players, races, passiveRounds, noFarmRounds, planets;
}
