using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceToFinishManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI p1distance, p2distance, p3distance, p4distance, p5distance, p6distance, p7distance, p8distance, p9distance, p10distance, p11distance;
    [SerializeField] List<TextMeshProUGUI> playerDistances;

    [SerializeField] TextMeshProUGUI countdowntext;
    public static DistanceToFinishManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        initializeList();
    }

    // Update is called once per frame
    void Update()
    {

    }
    int position = 0;
    public void RegisterDistanceToFinish(int ID, float distance, int speed)
    {
        if (distance == 0)
        {
            position++;
            if (position == 1) playerDistances[ID].text = position.ToString() + "st";
            else if (position == 2) playerDistances[ID].text = position.ToString() + "nd";
            else if (position == 3) playerDistances[ID].text = position.ToString() + "rd";
            else if (position > 3) playerDistances[ID].text = position.ToString() + "th";
        }
        else 
        {
            playerDistances[ID].text = ((int)distance).ToString() + "  Speed: " + speed.ToString();
        }
        
    }
    void initializeList()
    {
        playerDistances = new List<TextMeshProUGUI>
        {
            p1distance, p2distance, p3distance, p4distance, p5distance,
            p6distance, p7distance, p8distance, p9distance, p10distance, p11distance
        };
    }
    public void Countdown(int CTime)
    {
        StartCoroutine(CountdownCoroutine(CTime));
    }
    private IEnumerator CountdownCoroutine(int CTime)
    {
        while (CTime > 0)
        {
            countdowntext.text = CTime.ToString();
            yield return new WaitForSeconds(1f);
            CTime--;
        }

        countdowntext.text = "0"; // Ensure it displays 0 at the end
    }
}
