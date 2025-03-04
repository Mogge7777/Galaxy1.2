using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "CompetitionSpawning/TrackData", fileName = "NewTrackData")]
public class TrackData : ScriptableObject
{
    public float lenght, width, height;
    public GameObject trackPrefab;
}
