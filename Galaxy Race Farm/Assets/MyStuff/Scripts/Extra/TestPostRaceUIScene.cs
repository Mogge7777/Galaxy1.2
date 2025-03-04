using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPostRaceUIScene : MonoBehaviour
{
    List<GameObject> gameObjects = new List<GameObject>();
    List<float> times = new List<float>();

    float t1 = 1.0f, t2 = 24f, t3 = 16f, t4 = 132f;
    void Start()
    {
        Debug.Log("Start1");

        GameObject nr1 = new GameObject();
        GameObject nr2 = new GameObject();
        GameObject nr3 = new GameObject();
        GameObject nr4 = new GameObject();

        gameObjects = new List<GameObject> { nr1, nr2, nr3, nr4 };
        times = new List<float> { t1, t2, t3, t4 };
        if (UIManager.Instance == null)
        {
            Debug.LogError("UIManager Instance is NULL!");
        }
        else
        {
            UIManager.Instance.ShowResults(gameObjects, times);
        }
        Debug.Log("START 2");
    }


    void Update()
    {
        
    }
}
