using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public List<string> orderNames;
    public float timeUntilAngry = 200;
    public float lowestTimeUntilAngry = 100;
    public string selectedOrder;
    public bool hasOrdered;
    public GameObject sandwich;
    public GameObject steak;
    public GameObject pasta;
    public GameObject orderShowcaseLocation;

    private void Awake()
    {
        int rand = Random.Range(0, orderNames.Count);
        selectedOrder = orderNames[rand];
        timeUntilAngry = Mathf.Max(timeUntilAngry - 5 * LifeTracker.difficulty, lowestTimeUntilAngry);
    }

    private void Update()
    {
        if(timeUntilAngry > 0)
        {
            timeUntilAngry -= Time.deltaTime;
        }

        if(timeUntilAngry < 0)
        {
            LifeTracker.life -= 1;
            Destroy(gameObject);
        }
    }
}
