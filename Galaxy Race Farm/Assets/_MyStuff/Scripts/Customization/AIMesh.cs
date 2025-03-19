using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMesh : MonoBehaviour
{
    public List<GameObject> Meshes = new List<GameObject>();

    public int id;
    void Start()
    {
        id = GetComponent<AIRaceMovement>().playerID;
        meshing(); 
    }

    void meshing()
    {
        int seedNR = ((int)Input.mousePosition.x * id * id * 76646) + ((int)Input.mousePosition.y * id * id * id * id * 64999) - ((int)Input.mousePosition.z * id * id * id * id * 74756);
        UnityEngine.Random.InitState(seedNR);

        int randomMeshNR = UnityEngine.Random.Range(0, 700);

        switch (randomMeshNR)
        {
            case < 100:
                Meshes[0].gameObject.SetActive(true);
                break;
            case < 200:
                Meshes[1].gameObject.SetActive(true);
                break;
            case < 300:
                Meshes[2].gameObject.SetActive(true);
                break;
            case < 400:
                Meshes[3].gameObject.SetActive(true);
                break;
            case < 500:
                Meshes[4].gameObject.SetActive(true);
                break;
            case < 600:
                Meshes[5].gameObject.SetActive(true);
                break;
            case < 701:
                Meshes[6].gameObject.SetActive(true);
                break;

            default:
                break;
        }

    }
    void Update()
    {
        
    }
}
