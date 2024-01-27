using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public GameObject ingredientPrefab;
    public GameObject keyIndicator;
    public GameObject player;

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            keyIndicator.SetActive(true);
        }
    }
}
