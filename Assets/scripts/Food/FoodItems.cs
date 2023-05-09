using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItems : MonoBehaviour
{
    public GameObject[] foodPrefabs;

    public GameObject InstantiateFoodPrefab(int index, Transform parent)
    {
        GameObject newFood = Instantiate(foodPrefabs[index], parent);
        return newFood;
    }

    public GameObject checkFood(GameObject selectedObject, Transform parent)
    {
        GameObject newFood = null;
        if (selectedObject.GetComponent<Dough>() != null)
            newFood = InstantiateFoodPrefab(0, parent);

        else if (selectedObject.GetComponent<Lettuce>() != null)
            newFood = InstantiateFoodPrefab(1, parent);

        else if (selectedObject.GetComponent<Meat>() != null)
            newFood = InstantiateFoodPrefab(2, parent);

        else if (selectedObject.GetComponent<Tomato>() != null)
            newFood = InstantiateFoodPrefab(3, parent);

        else
            Debug.Log("Nothing");
        newFood.transform.localPosition = new Vector3(0f, 0f, 0.4f);
        return newFood;
    }

    public GameObject changeState(GameObject selectedObject, Transform parent)
    {   
        //Debug.Log(selectedObject.name);
        GameObject newFood = null;

        if (selectedObject.name == "dough(Clone)")
            newFood = InstantiateFoodPrefab(4, parent);

        else if (selectedObject.name == "Lettuce(Clone)")
            newFood = InstantiateFoodPrefab(5, parent);

        else if (selectedObject.name == "Meat(Clone)")
            newFood = InstantiateFoodPrefab(6, parent);

        else if (selectedObject.name == "Tomato(Clone)")
            newFood = InstantiateFoodPrefab(7, parent);

        else
            Debug.Log("Nothing");

        Destroy(selectedObject);
        newFood.transform.localPosition = new Vector3(0f, 1.0f, 0f);
        return newFood;
    }
}


