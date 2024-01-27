using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInv : MonoBehaviour
{
    public InputAction interactAction;
    public float radius = 2;
    public List<GameObject> ingredients = new List<GameObject>();
    public GameObject ingredient1;
    public GameObject ingredient1Prefab;
    public GameObject ingredient2;
    public GameObject ingredient2Prefab;
    public GameObject recipe;
    public GameObject recipePrefab;

    private void Update()
    {
        //Interact with ingredients
        var nearbyIngredients = Physics.OverlapSphere(transform.position, radius).Where(x => x.GetComponent<Ingredient>() != null);
        if (nearbyIngredients.Count() > 0 && interactAction.WasPressedThisFrame())
        {
            foreach (Collider ingredient in nearbyIngredients)
            {
                var ingredientScript = ingredient.gameObject.GetComponent<Ingredient>();
                if (ingredients.Count < 2)
                {
                    ingredients.Add(ingredientScript.ingredientPrefab);
                    if (ingredient1Prefab == null)
                    {
                        ingredient1Prefab = Instantiate(ingredients[0]);
                        ingredient1Prefab.transform.parent = ingredient1.transform;
                        ingredient1Prefab.transform.localPosition = Vector3.zero;
                        ingredient1Prefab.transform.rotation = ingredient1.transform.rotation;
                    }
                    else if (ingredient2Prefab == null)
                    {
                        ingredient2Prefab = Instantiate(ingredients[1]);
                        ingredient2Prefab.transform.parent = ingredient2.transform;
                        ingredient2Prefab.transform.localPosition = Vector3.zero;
                        ingredient2Prefab.transform.rotation = ingredient2.transform.rotation;
                    }
                }
            }
        }

        //Interact with pots
        var nearbyPots = Physics.OverlapSphere(transform.position, radius).Where(x => x.GetComponent<Pot>() != null);
        if (nearbyPots.Count() > 0 && interactAction.WasPressedThisFrame())
        {
            var closestPot = nearbyPots.First().gameObject.GetComponent<Pot>();
            if (closestPot.readyToCollect)
            {
                recipePrefab = Instantiate(closestPot.currentObject);
                recipePrefab.transform.parent = recipe.transform;
                recipePrefab.transform.localPosition = Vector3.zero;
                recipePrefab.transform.rotation = recipe.transform.rotation;
            }
            else
            {
                if (ingredients.Count > 0)
                {
                    closestPot.ingredients.Add(ingredients[0]);
                    ingredients.RemoveAt(0);
                    if (ingredients.Count > 0)
                    {
                        Destroy(ingredient1Prefab);
                        ingredient1Prefab = null;
                    }
                    else if (ingredient2Prefab != null)
                    {
                        Destroy(ingredient2Prefab);
                        ingredient2Prefab = null;
                    }
                    else
                    {
                        Destroy(ingredient1Prefab);
                        ingredient1Prefab = null;
                    }
                }
            }
        }


        //Interact with customers

        var nearbyCustomers = Physics.OverlapSphere(transform.position, radius).Where(x => x.GetComponent<Customer>() != null);
        if(nearbyCustomers.Count() > 0 && interactAction.WasPressedThisFrame())
        {
            var closestCustomer = nearbyCustomers.First().gameObject.GetComponent<Customer>();
            if(!closestCustomer.hasOrdered)
            {
                switch(closestCustomer.selectedOrder)
                {
                    case "Sandwich":
                        var sandwich = Instantiate(closestCustomer.sandwich);
                        sandwich.transform.parent = closestCustomer.orderShowcaseLocation.transform;
                        sandwich.transform.localPosition = Vector3.zero;
                        sandwich.transform.rotation = closestCustomer.orderShowcaseLocation.transform.rotation;
                        break;
                    case "Steak":
                        var steak = Instantiate(closestCustomer.steak);
                        steak.transform.parent = closestCustomer.orderShowcaseLocation.transform;
                        steak.transform.localPosition = Vector3.zero;
                        steak.transform.rotation = closestCustomer.orderShowcaseLocation.transform.rotation;
                        break;
                    case "Pasta":
                        var pasta = Instantiate(closestCustomer.pasta);
                        pasta.transform.parent = closestCustomer.orderShowcaseLocation.transform;
                        pasta.transform.localPosition = Vector3.zero;
                        pasta.transform.rotation = closestCustomer.orderShowcaseLocation.transform.rotation;
                        break;
                }
                closestCustomer.hasOrdered = true;
            } else if(recipePrefab != null)
            {
                string cutRecipePrefabName = recipePrefab.name.Replace("(Clone)", "");
                if (cutRecipePrefabName == closestCustomer.selectedOrder)
                {
                    Destroy(closestCustomer.gameObject);
                    Destroy(recipePrefab.gameObject);
                    recipePrefab = null;
                }
            }
        }
    }

    private void OnEnable()
    {
        interactAction.Enable();
    }

    private void OnDisable()
    {
        interactAction.Disable();
    }
}
