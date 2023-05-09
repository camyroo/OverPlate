using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipies : MonoBehaviour
{

   Dictionary<string, List<string>> ingredientToDishMap = new Dictionary<string, List<string>>()
    {
        { "Pizza", new List<string> { "cutDough(Clone)", "cutTomato(Clone)"} },
        { "Steak", new List<string> { "cutMeat(Clone)", "cutMeat(Clone)"} },
        { "Salad", new List<string> { "cutTomato(Clone)", "lettuce(Clone)" } },
        // Add more ingredient-to-dish mappings as needed
    };
    public void CheckDish(GameObject food1, GameObject food2)
    {
        Debug.Log(food1.name);
        Debug.Log(food2.name);
        bool isMatch = false;
        string dishName = "";

        foreach (KeyValuePair<string, List<string>> kvp in ingredientToDishMap)
        {
            List<string> ingredients = kvp.Value;

            if (ingredients.Contains(food1.name) && ingredients.Contains(food2.name))
            {
                isMatch = true;
                dishName = kvp.Key;
                break;
            }
        }

        if (isMatch)
        {
            Debug.Log("The dish is: " + dishName);
        }
        else
        {
            Debug.Log("No dish matches the ingredient combination.");
        }
    }

    private void InstantiatePizza()
    {
        // Instantiate a pizza GameObject here
        Debug.Log("Pizza instantiated!");
    }
}
