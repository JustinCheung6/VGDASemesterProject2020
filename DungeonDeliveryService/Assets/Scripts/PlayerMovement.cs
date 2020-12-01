using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Set user moveSpeed for DeliveryPerson.
    public float moveSpeed = 5.0f;
    // Get the Object for the DeliveryPerson
    public Rigidbody2D rb;
    public float acceleration = 1.0f;
    // Set up the movement variables that will be called.
    private float moveHorizontal, moveVertical;
    [SerializeField] private bool animatedMovement = false;
    //Check if player is moving by the controller
    private bool controllerMovement = false;
    public float platformMove;

    public bool IsMoving { get => (animatedMovement || controllerMovement); }

    // Checks if direction is disabled because of wind: 0 = Left, 1 = Down, 2 = Right, 3 = Up
    [SerializeField] private int[] restrinctions = { 0, 0, 0, 0 };

    //Stops player movement at a specfic direction
    public void AddRestrictions(int index)
    {
        restrinctions[index] += 1;
    }
    //Stops player movement altogether
    public void AddRestrictions()
    {
        rb.velocity = Vector3.zero;

        for (int i = 0; i < restrinctions.Length; i++)
        {
            restrinctions[i] += 1;
        }
    }
    //Removes a restrction from player movement at a specific direction
    public void RemoveRestrictions(int index)
    {
        restrinctions[index] -= 1;
    }
    //Removes a restrction from player movement in all directions
    public void RemoveRestrictions()
    {
        for (int i = 0; i < restrinctions.Length; i++)
        {
            restrinctions[i] -= 1;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector2 currentPosition = transform.position;
    } 

    // Update is called once per frame
    void Update()
    {
        if (!animatedMovement)
        {
            // Get the respective Horizontal and Vertical movement.
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        
    } // Close Update

    private void FixedUpdate()
    {
        if (!animatedMovement)
        {
            // Calculate the player's net movement.
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            //Check if player is moving at all
            if (movement == Vector2.zero)
            {
                controllerMovement = false;
            }
            else
            {
                controllerMovement = true;

                //Set movement to zero if movement is restricted in a specific direction
                if (movement.x < 0 && restrinctions[0] > 0)
                    movement.x = 0;
                else if (movement.x > 0 && restrinctions[2] > 0)
                    movement.x = 0;
                else
                    //This prevents player from being affected by area effector after it's been turned off
                    rb.velocity = new Vector2(0, rb.velocity.y);

                if (movement.y < 0 && restrinctions[1] > 0)
                    movement.y = 0;
                else if (movement.y > 0 && restrinctions[3] > 0)
                    movement.y = 0;
                else
                    //This prevents player from being affected by area effector after it's been turned off
                    rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            // Apply the actual movement
            transform.position = ((Vector2)transform.position) + (movement * moveSpeed * Time.fixedDeltaTime * 10f);
        }
    } 

    public void WalkToDoor(Vector2 destination)
    {
        transform.position = destination;
    }
} //Close PlayerMovement
