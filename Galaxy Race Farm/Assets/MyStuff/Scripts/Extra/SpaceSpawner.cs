using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab, parentHolder;

    [SerializeField] int positiveOrNegative = 1;

    [SerializeField] int minDistanceZ = 3, maxDistanceZ = 25;

    [SerializeField] float size = 1;

    [SerializeField] TrackData laneData;
    [SerializeField] int closestFirstPrefabChance, furthestFirstPrefaChance;
    [SerializeField] int yChangeLow, yChangeHigh;

    void Start()
    {
        //fixing, not part of script
        if(minDistanceZ == 0) minDistanceZ = 20;
        if (maxDistanceZ == 0) maxDistanceZ = 200;
        if (positiveOrNegative == 0) positiveOrNegative = 1;

        var trackLenght = laneData.lenght;
        Vector3 position = transform.position;
        parentHolder = new GameObject("parentToSpace");

        for (int i = Random.Range(closestFirstPrefabChance, furthestFirstPrefaChance  ); i < trackLenght; i += Random.Range(minDistanceZ, maxDistanceZ))
        {
            GameObject SpaceObject = Instantiate(prefab, transform.position + 
                new Vector3(transform.position.x + positiveOrNegative * Random.Range(10, 50), Random.Range(yChangeLow, yChangeHigh), transform.position.z + i), Quaternion.identity);
            SpaceObject.transform.parent = parentHolder.transform;

            SpaceObject.transform.localScale = new Vector3(size, size, size);
        }
        //for (int i = 0; i < trackLenght; i += Random.Range(minDistanceZ, maxDistanceZ))
        //{
        //    GameObject SpaceObject = Instantiate(prefab, transform.position + new Vector3(0, 0, transform.position.z + i), Quaternion.identity);
        //    SpaceObject.transform.parent = parentHolder.transform;
        //}

    }
}
