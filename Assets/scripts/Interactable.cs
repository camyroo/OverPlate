using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public PlayerController player; 

    void Start()
    {
        player = GetComponent<PlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
