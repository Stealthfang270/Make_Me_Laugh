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

    private void Update()
    {
        var nearbyIngredients = Physics.OverlapSphere(transform.position, radius).Where(x => x.GetComponent<Ingredient>() != null);
        if (nearbyIngredients.Count() > 0 && interactAction.WasPressedThisFrame())
        {
            foreach (Collider ingredient in nearbyIngredients)
            {
                var ingredientScript = ingredient.gameObject.GetComponent<Ingredient>();
                if (ingredients.Count < 2)
                {
                    ingredients.Add(ingredientScript.ingredientPrefab);
                }
            }
        }

        var nearbyPots = Physics.OverlapSphere(transform.position, radius).Where(x => x.GetComponent<Pot>() != null);
        if(nearbyPots.Count() > 0 && interactAction.WasPressedThisFrame())
        {
            var closestPot = nearbyPots.First().gameObject.GetComponent<Pot>();
            if(ingredients.Count > 0)
            {
                closestPot.ingredients.Add(ingredients[0]);
                ingredients.RemoveAt(0);
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
