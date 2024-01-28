using MagicPigGames;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public List<GameObject> ingredients = new List<GameObject>();
    public List<string> ingredientNames = new List<string>();
    public GameObject keyIndicator;
    public GameObject player;

    public Recipe sandwich;
    public Recipe pasta;
    public Recipe steak;

    public List<string> sandwichIngredients;
    public float sandwichTime;
    public List<string> pastaIngredients;
    public float pastaTime;
    public List<string> steakIngredients;
    public float steakTime;
    public GameObject steakObject;
    public GameObject sandwichObject;
    public GameObject pastaObject;
    public GameObject currentObject;

    public bool isCooking;
    public bool readyToCollect;
    public float cookTime;

    public float explodeTimeLeft = 30;
    public float maxExplodeTime = 30;

    public HorizontalProgressBar bar;
    public GameObject progressBarCanvas;

    public GameObject explosionPrefab;

    private void Awake()
    {
        sandwich = new Recipe();
        sandwich.ingredients = sandwichIngredients;
        sandwich.cookTime = sandwichTime;
        pasta = new Recipe();
        pasta.ingredients = pastaIngredients;
        pasta.cookTime = pastaTime;
        steak = new Recipe();
        steak.ingredients = steakIngredients;
        steak.cookTime = steakTime;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            keyIndicator.SetActive(true);
        }

        ingredientNames = ingredients.Select(x => x.name).ToList();

        if (ListEqual(ingredientNames, sandwich.ingredients) && !isCooking && !readyToCollect)
        {
            isCooking = true;
            cookTime = sandwich.cookTime;
            currentObject = sandwichObject;
        }
        if (ListEqual(ingredientNames, steak.ingredients) && !isCooking && !readyToCollect)
        {
            isCooking = true;
            cookTime = steak.cookTime;
            currentObject = steakObject;
        }
        if (ListEqual(ingredientNames, pasta.ingredients) && !isCooking && !readyToCollect)
        {
            isCooking = true;
            cookTime = pasta.cookTime;
            currentObject = pastaObject;
        }

        if (isCooking && cookTime > 0)
        {
            cookTime -= Time.deltaTime;
        }
        else if (isCooking && cookTime < 0)
        {
            isCooking = false;
            readyToCollect = true;
        }

        if (ingredients.Count > 0)
        {
            explodeTimeLeft -= Time.deltaTime;
            if (explodeTimeLeft < 0)
            {
                explodeTimeLeft = maxExplodeTime;
                if (Vector3.Distance(transform.position, player.transform.position) < 10)
                {
                    player.GetComponent<RagdollSpawner>().RagDoll();
                }
                var nearbyObjects = Physics.OverlapSphere(transform.position, 10);
                var explode = Instantiate(explosionPrefab);
                explode.transform.position = transform.position;
                foreach (Collider collider in nearbyObjects)
                {
                    Rigidbody rb = collider.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddExplosionForce(10000, transform.position, 10, 5000);
                    }
                }
                ingredients.Clear();
            }
        }

        if (isCooking)
        {
            progressBarCanvas.SetActive(true);
            bar.SetProgress(Mathf.Clamp01(cookTime / steakTime));
        }
        else
        {
            progressBarCanvas.SetActive(false);
        }
    }

    public bool ListEqual(List<string> list1, List<string> list2)
    {
        list1.Sort();
        list2.Sort();

        return list1.SequenceEqual(list2);
    }
}

public struct Recipe
{
    public List<string> ingredients;
    public float cookTime;
}
