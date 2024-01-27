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
                    if(ingredient1Prefab == null)
                    {
                        ingredient1Prefab = Instantiate(ingredients[0]);
                        ingredient1Prefab.transform.parent = ingredient1.transform;
                        ingredient1Prefab.transform.localPosition = Vector3.zero;
                        ingredient1Prefab.transform.rotation = ingredient1.transform.rotation;
                    } else if (ingredient2Prefab == null)
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
            if (ingredients.Count > 0)
            {
                closestPot.ingredients.Add(ingredients[0]);
                ingredients.RemoveAt(0);
                if (ingredients.Count > 0)
                {
                    Destroy(ingredient1Prefab);
                    ingredient1Prefab = null;
                }
                else if(ingredient2Prefab != null)
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

    private void OnEnable()
    {
        interactAction.Enable();
    }

    private void OnDisable()
    {
        interactAction.Disable();
    }
}
