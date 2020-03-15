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
    float moveHorizontal, moveVertical;

    // Checks if direction is disabled because of wind: 0 = Left, 1 = Down, 2 = Right, 3 = Up
    [SerializeField]private int[] restrinctions = { 0, 0, 0, 0 };

    public void AddRestriction(int index)
    {
        restrinctions[index] += 1;
    }
    public void AddRestriction()
    {
        for (int i = 0; i < restrinctions.Length; i++)
        {
            restrinctions[i] += 1;
        }
    }

    public void RemoveRestriction(int index)
    {
        restrinctions[index] += 1;
    }
    public void RemoveRestriction()
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
    } //Close Start

    // Update is called once per frame
    void Update()
    {
        // Get the respective Horizontal and Vertical movement.
        moveHorizontal = Input.GetAxis("Horizontal");

        moveVertical = Input.GetAxis("Vertical");
    } // Close Update

    private void FixedUpdate()
    {
        // Calculate the player's net movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Set movement to zero if player is going against wind direction
        if (movement.x < 0 && restrinctions[0] > 0)
            movement.x = 0;
        if (movement.x > 0 && restrinctions[2] > 0)
            movement.x = 0;

        if (movement.y < 0 && restrinctions[1] > 0)
            movement.y = 0;
        if (movement.y > 0 && restrinctions[3] > 0)
            movement.y = 0;

        // Apply the actual movement
        transform.position = ((Vector2) transform.position) + (movement * moveSpeed * Time.fixedDeltaTime * 10f);
    } //Close FixedUpdate
} //Close PlayerMovement
