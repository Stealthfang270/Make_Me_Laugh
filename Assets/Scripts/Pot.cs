using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public List<GameObject> ingredients = new List<GameObject>();
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

public struct Recipe
{
    public List<GameObject> ingredients;
    public float cookTime;
}
