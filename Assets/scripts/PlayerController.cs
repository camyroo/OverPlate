using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f; // variables for movement
    public float bumpSpeed = 2.0f;
    public float turnSpeed = 700f;
    private Vector3 movementDirection;

    public float dash = 3.75f; // variables for dash
    public float nextFiretime = 0;
    public float coolDownDash = 0.5f;
    
    public bool canDash = false;
    public bool pDash = false;
    public bool pInteract = false; // Whether or not the player is currently interacting
    public bool pPickUp = false;

    public GameObject otherPlayer;

    public Slider ActionSlider;

    public PlayerInputHandler handler;
    private CharacterController controller;

    //public event System.Action<int> OnScoreChanged;

    public enum playerName
    {
        blue,
        red,
        green,
        purple,
    }

    public playerName thisPlayersName = playerName.blue;

    private void Awake()
    {
        handler = GetComponent<PlayerInputHandler>();
        controller = GetComponent<CharacterController>();

    }

    private void FixedUpdate()
    {
        //basic movement
        movementDirection.Normalize();
        controller.Move(movementDirection * speed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                toRotation,
                turnSpeed * Time.deltaTime
            );
        }
    }

    public void Update()
    {
        if (pDash && Time.time > nextFiretime && canDash && movementDirection.magnitude != 0)
        {
            canDash = false;
            speed += dash;
            nextFiretime = Time.time + coolDownDash;
        }

        if (Time.time > nextFiretime)
        {
            speed = 3.0f;
        }

        if (Time.time > nextFiretime + 0.25f)
        {
            canDash = true;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        movementDirection = new Vector3(movement.x, 0, movement.y); //gets the current direction of movement
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        pDash = context.ReadValueAsButton();
        //Debug.Log(pDash);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
            pInteract = true;

        if (context.performed)
            pInteract = false;

        if (context.canceled)
        {
            pInteract = false;
            ActionSlider.gameObject.SetActive(false);
        }
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        if (context.performed)
            pPickUp = context.ReadValueAsButton();
        else if (context.canceled)
            pPickUp = context.ReadValueAsButton();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            otherPlayer = hit.gameObject;
            Vector3 pushDirection = otherPlayer.transform.position - transform.position;
            pushDirection.y = 0.0f; // ensure that the push is only in the horizontal plane
            pushDirection = pushDirection.normalized;

            float pushForce = speed * Time.deltaTime;
            float time = 0.1f;
            float duration = 0.15f; // adjust this value to control the smoothness of the push

            while (time < duration)
            {
                float t = time / duration;
                float currentPushForce = Mathf.Lerp(pushForce, 0.0f, t);
                otherPlayer
                    .GetComponent<CharacterController>()
                    .Move(pushDirection * currentPushForce);
                time += Time.deltaTime;
            }
        }
    }
}
