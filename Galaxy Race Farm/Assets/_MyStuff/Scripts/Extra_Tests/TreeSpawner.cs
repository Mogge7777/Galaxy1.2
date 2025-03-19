using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab, parentHolder;

    [SerializeField] int positiveOrNegative = 1;

    [SerializeField] int minDistanceZ = 3, maxDistanceZ = 25;

    [SerializeField] TrackData laneData;

    void Start()
    {
        //fixing, not part of script
        if(minDistanceZ == 0) minDistanceZ = 20;
        if (maxDistanceZ == 0) maxDistanceZ = 200;
        if (positiveOrNegative == 0) positiveOrNegative = 1;
        //clear

        var trackLenght = laneData.lenght;
        Vector3 position = transform.position;
        parentHolder = new GameObject("parentToTrees");

        for (int i = 40; i < trackLenght; i += Random.Range(minDistanceZ, maxDistanceZ))
        {
            GameObject Tree = Instantiate(prefab, transform.position + new Vector3(transform.position.x + positiveOrNegative * Random.Range(10, 30), 0, transform.position.z + i), Quaternion.identity);
            Tree.transform.parent = parentHolder.transform;
        }
        for (int i = 0; i < trackLenght; i += Random.Range(minDistanceZ, maxDistanceZ))
        {
            GameObject Tree = Instantiate(prefab, transform.position + new Vector3(0, 0, transform.position.z + i), Quaternion.identity);
            Tree.transform.parent = parentHolder.transform;
        }

    }
}
