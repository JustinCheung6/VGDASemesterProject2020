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
    private bool animatedMovement = false;


    // Checks if direction is disabled because of wind: 0 = Left, 1 = Down, 2 = Right, 3 = Up
    private int[] restrinctions = { 0, 0, 0, 0 };

    //Stops player movement at a specfic direction
    public void AddRestrictions(int index)
    {
        restrinctions[index] += 1;
    }
    //Stops player movement altogether
    public void AddRestrictions()
    {
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

            //Set movement to zero if movement is restricted in a specific direction
            if (movement.x < 0 && restrinctions[0] > 0)
                movement.x = 0;
            if (movement.x > 0 && restrinctions[2] > 0)
                movement.x = 0;

            if (movement.y < 0 && restrinctions[1] > 0)
                movement.y = 0;
            if (movement.y > 0 && restrinctions[3] > 0)
                movement.y = 0;

            // Apply the actual movement
            transform.position = ((Vector2)transform.position) + (movement * moveSpeed * Time.fixedDeltaTime * 10f);
        }
    } 

    public IEnumerator WalkToDoor(Vector2 destination)
    {
        AddRestrictions();

        Vector2 startingPos = transform.position;
        animatedMovement = true;

        while (animatedMovement)
        {
            Vector2 movement = Vector2.zero;
            Debug.Log("We Moving");

            if ((Vector2)transform.position !=  destination)
            {
                transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * 0.1f);
                yield return new WaitForFixedUpdate();
            }
            else
            {
                animatedMovement = false;
                RemoveRestrictions();
            }
        }
    }
} //Close PlayerMovement
