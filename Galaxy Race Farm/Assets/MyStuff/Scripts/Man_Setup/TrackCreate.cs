using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCreate : MonoBehaviour
{
    [SerializeField] GameObject trackPrefab, track, leftTrackSeparator, rightTrackSeparator, startLine, finishLine, startWP, finishWP;
    [SerializeField] TrackData laneData;
    void Start()
    {
        //trackPos position transform
        Vector3 startPos = transform.position;
        track.transform.position = startPos + new Vector3(0f, 0f, laneData.lenght/2);
        track.transform.localScale = new Vector3(laneData.width, laneData.height, laneData.lenght);

        //trackseparators treansform
        leftTrackSeparator.transform.localScale = new Vector3(leftTrackSeparator.transform.localScale.x / laneData.width, 1.03f, 1);
        rightTrackSeparator.transform.localScale = new Vector3(rightTrackSeparator.transform.localScale.x / laneData.width, 1.03f, 1);

        //startline transform
        startLine.transform.position = startPos + new Vector3(0f, 0.01f, 5f);
        startLine.transform.localScale = new Vector3(laneData.width, 0.01f, 2f);

        //finishline transform
        finishLine.transform.position = startPos + new Vector3(0, 0.01f, laneData.lenght - 10f);
        finishLine.transform.localScale = new Vector3(laneData.width, 0.01f, 2f);

        //wps?
        startWP.transform.position = startPos + new Vector3(0f, 0.01f, 5f);
        startWP.transform.localScale = new Vector3(laneData.width, 2f, 6f);

        //finishline transform
        finishWP.transform.position = startPos + new Vector3(0, 0.01f, laneData.lenght - 10f);
        finishWP.transform.localScale = new Vector3(laneData.width, 2f, 6f);
    }

    void Update()
    {
        
    }
}
