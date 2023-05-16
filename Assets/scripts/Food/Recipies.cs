using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipies : MonoBehaviour
{

    Dictionary<string, List<string>> ingredientToDishMap = new Dictionary<string, List<string>>()
    {
        { "Pizza", new List<string> { "cutDough(Clone)", "cutTomato(Clone)"} },
        { "Steak", new List<string> { "cutMeat(Clone)", "cutMeat(Clone)"} },
        { "Salad", new List<string> { "cutTomato(Clone)", "Lettuce(Clone)" } },
    };
    public string CheckDish(GameObject food1, GameObject food2)
    {
        //Debug.Log(food1.name + " : " + food2.name);
        bool isMatch = false;
        string dishName = "";

        foreach (KeyValuePair<string, List<string>> kvp in ingredientToDishMap)
        {
            List<string> ingredients = kvp.Value;
            //Debug.Log("Checking dish: " + kvp.Key);
            //Debug.Log("Required ingredients: " + string.Join(", ", ingredients.ToArray()));

            // Check all possible combinations of food1 and food2
            if (ingredients.Contains(food1.name) && ingredients.Contains(food2.name) ||
                ingredients.Contains(food2.name) && ingredients.Contains(food1.name))
            {
                isMatch = true;
                dishName = kvp.Key;
                break;
            }
        }

        if (isMatch)
        {
            Destroy(food1);
            Destroy(food2);
            return dishName;
            //Debug.Log("The dish is: " + dishName);
        }
        else
        {
            Debug.Log("No dish matches the ingredient combination.");
            return dishName;
        }
    }
}
