using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject customer;
    public float timeLeft = 30;
    public float maxTime = 30;

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = maxTime;
            AttemptSpawn();
        }
    }

    public void AttemptSpawn()
    {
        if(customer == null)
        {
            customer = Instantiate(prefab);
            customer.transform.parent = transform;
            customer.transform.localPosition = Vector3.zero;
            customer.transform.localRotation = Quaternion.identity;
        }
    }
}
