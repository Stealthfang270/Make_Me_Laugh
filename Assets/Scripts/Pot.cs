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

        if(ListEqual(ingredientNames, sandwich.ingredients))
        {
            Debug.Log("You made sandwich!");
        }
        if(ListEqual(ingredientNames, steak.ingredients))
        {
            Debug.Log("You made a steak!");
        }
        if (ListEqual(ingredientNames, pasta.ingredients))
        {
            Debug.Log("You made pasta!");
        }
    }

    public bool ListEqual(List<string> list1, List<string> list2)
    {
        list1.OrderBy(x => x);
        list2.OrderBy(x => x);

        return list1.SequenceEqual(list2);
    }
}

public struct Recipe
{
    public List<string> ingredients;
    public float cookTime;
}
