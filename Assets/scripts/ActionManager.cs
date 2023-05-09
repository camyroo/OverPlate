using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public PlayerController player;
    public Score score;
    public Raycast ray;
    public GameObject ObjectIWantToPickUp;
    public FoodItems Food;
    public Recipies recipie;
    public bool canPickorDrop = false;
    public bool hasItem = false;

    public bool interacting = false;

    public float interactDuration = 2.0f; // The time it takes to complete the interaction
    public float currentInteractTime = 0.0f; // The current time spent interacting

    void Start()
    {
        // Find the PlayerController component on the same game object as this script
        player = GetComponent<PlayerController>();
        Food = FindObjectOfType<FoodItems>();
        recipie = FindObjectOfType<Recipies>();
        // Find the Raycast component on a child object named "Raycast"
        Transform raycastTransform = transform.Find("Raycast");
        if (raycastTransform != null)
        {
            ray = raycastTransform.GetComponent<Raycast>();
        }
    }

    void FixedUpdate()
    {
        canPickorDrop = ray.hitting;
        //Debug.Log(player.pPickUp);

        tryDish();
        put();
        pick();
        interact();

        Vector3 sliderPosition = transform.localPosition + new Vector3(0f, 1f, 0f);
        player.ActionSlider.transform.position = Camera.main.WorldToScreenPoint(sliderPosition);

        if (interacting)
        {
            currentInteractTime += Time.deltaTime;
            float sliderValue = currentInteractTime / interactDuration;
            sliderValue = Mathf.Clamp01(sliderValue); // Make sure sliderValue is between 0 and 1
            player.ActionSlider.value = sliderValue;
        }

        //Debug.Log(player.ActionSlider.value);
    }

    void pick()
    {
        // picking item up
        if (canPickorDrop && !hasItem && ray._selection != null)
        {
            ObjectIWantToPickUp = null;
            if (player.pPickUp)
            { // If the ray from the character is hitting a selectable object
                player.pPickUp = false;
                if (
                    ray._selection.transform.childCount > 0
                    && ray._selection.transform.GetChild(0).gameObject.CompareTag("Food")
                )
                { //If the object that the player is selecting has food
                    ObjectIWantToPickUp = ray._selection.transform.GetChild(0).gameObject;
                    ObjectIWantToPickUp.transform.SetParent(player.transform);
                    ObjectIWantToPickUp.transform.localPosition = new Vector3(0f, 0.2f, 0.4f);
                    hasItem = true;
                    return;
                }
                else if (ray._selection.gameObject.GetComponent<FoodBox>() != null)
                {
                    ObjectIWantToPickUp = Food.checkFood(
                        ray._selection.gameObject,
                        player.transform
                    );
                    hasItem = true;
                    return;
                }
            }
        }
    }

    void put()
    {
        //putting item down
        if (hasItem && canPickorDrop && ray._selection != null)
        { // If the ray from the character is hitting a selectable object
            if (player.pPickUp)
            {
                player.pPickUp = false;
                if (ray._selection.gameObject.name == "Trash" && ObjectIWantToPickUp.CompareTag("Food"))
                {
                    destroy();
                }
                if (ray._selection.gameObject.name == "GOAL")
                {
                    destroy();
                    Debug.Log("INCREASE SCORE!");
                }
                else if (ray._selection.transform.childCount == 0 || ray._selection.gameObject.GetComponent<Appliance>() != null)
                { // if the selectable object doesn't have any food on it
                    ObjectIWantToPickUp.transform.SetParent(ray._selection.transform);
                    ObjectIWantToPickUp.transform.position = ray._selection.transform.position;
                    ObjectIWantToPickUp.transform.localPosition = new Vector3(0f, 1f, 0f);
                    hasItem = false;
                }
            }
        }


    }


    void destroy()
    {
        Destroy(ObjectIWantToPickUp);
        ObjectIWantToPickUp = null;
        hasItem = false;
    }

    void interact()
    {
        if (!hasItem && canPickorDrop && ray._selection != null)
        { // if playerhand is empty
            if (
                ray._selection.gameObject.GetComponent<CuttingBoard>() != null
                && ray._selection.transform.childCount > 0
                && ray._selection.transform.GetChild(0).gameObject.GetComponent<Food>() != null

            )
            {
                var food = ray._selection.transform.GetChild(0).gameObject.GetComponent<Food>();

                if (player.ActionSlider.value >= 0.9f && food.cutState == 0)
                {
                    Food.changeState(ray._selection.transform.GetChild(0).gameObject, ray._selection.transform);
                }

                if (player.pInteract == true && food.cutState == 0)
                {
                    interacting = true;
                    player.ActionSlider.gameObject.SetActive(true);
                }

                if (player.pInteract == false)
                {
                    interacting = false;
                    player.ActionSlider.gameObject.SetActive(false);
                    currentInteractTime = 0.0f;
                    player.ActionSlider.value = 0f;
                }
            }
        }
        else
        {
            interacting = false;
            player.ActionSlider.gameObject.SetActive(false);
            currentInteractTime = 0.0f;
            player.ActionSlider.value = 0f;
        }
    }

    void tryDish()
    {
        // if player has and item and wants to put and item on a counter that already has a food item on it
        if (hasItem && canPickorDrop && ray._selection.transform.childCount > 0 && ray._selection.transform.GetChild(0).gameObject.GetComponent<Food>() != null)
        {
            if (player.pPickUp == true)
            {
                recipie.CheckDish(ray._selection.transform.GetChild(0).gameObject, ObjectIWantToPickUp);
            }
        }

    }
}
